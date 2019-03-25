using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidly.Controllers
{
    using System.Data.Entity.Validation;

    using Vidly.Models;
    using Vidly.ViewModels;

    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            this._context = new ApplicationDbContext();
        }

        /// <summary>
        /// This object is disposable object,
        /// so we need to properly dispose this object
        /// so we override dispose method of this controller class
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
        }

        public ActionResult Index()
        {
            // var movies = this._context.Movies.Include(m => m.Genre).ToList();

            if (User.IsInRole("CanManageMovies"))
            {
                return this.View("List");
            }

            return this.View("ReadOnlyList");

        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genreTypes = this._context.Genres;

            var viewModel = new MovieFormViewModel
                                {
                                    Genres = genreTypes
                                };
            return this.View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = this._context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = new MovieFormViewModel(movie)
                                {
                                    Genres = this._context.Genres.ToList()
                                };
            return this.View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel (movie)
                                    {
                                        Genres = this._context.Genres.ToList()
                                    };
                return this.View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                movie.NumberAvailable = movie.NumberInStock;
                this._context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = this._context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            this._context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Details(int id)
        {
            var movie = this._context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return this.HttpNotFound();

            return this.View(movie);
        }


        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
                                {
                                    new Customer{Name = "John Smith"},
                                    new Customer{Name = "Marry Williams"}
                                };

            var viewModel = new RandomMovieViewModel
                                {
                                    Movie = movie,
                                    Customers = customers
                                };


            return View(viewModel);
        }
        

        public ActionResult TrialContent(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if (String.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";
            }

            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {

            return Content(year + "/" + month);
        }
    }
}