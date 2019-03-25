using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Vidly.Controllers.Api
{
    using AutoMapper;

    using Vidly.Dtos;
    using Vidly.Models;
    
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            this._context = new ApplicationDbContext();
        }

        // Get /api/movies
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesQuery = this._context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
            }

            var movieDtos = moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
            return movieDtos;
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = this._context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return this.NotFound();
            }

            return this.Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            this._context.Movies.Add(movie);
            this._context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // PUT api/movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var movieInDb = this._context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
            {
                return this.NotFound();
            }

            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);

            this._context.SaveChanges();

            return this.Ok();
        }

        // DELETE api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = this._context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
            {
                return this.NotFound();
            }

            this._context.Movies.Remove(movieInDb);
            this._context.SaveChanges();

            return this.Ok();
        }
    }
}
