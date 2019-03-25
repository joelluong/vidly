using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Vidly.Controllers.Api
{
    using Vidly.Dtos;
    using Vidly.Models;

    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            this._context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            // For public API, no Movie Ids
            /*if (newRental.MovieIds.Count == 0)
            {
                return this.BadRequest("No movie Ids have been given;");
            }*/

            // var customer = this._context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
            var customer = this._context.Customers.Single(c => c.Id == newRental.CustomerId);

            // For public API, CustomerId is invalid (1)
            /*if (customer == null)
            {
                return this.BadRequest("CustomerId is not valid.");
            }*/

            var movies = this._context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();

            // For public API, No MovieIds
            /*if (movies.Count != newRental.MovieIds.Count)
            {
                return this.BadRequest("One or more MovieIds are invalid");
            }*/

            foreach (var movie in movies)
            {
                // One or more movies are not available (4)
                if (movie.NumberAvailable == 0)
                {
                    return this.BadRequest("Movie is not available.");
                }

                movie.NumberAvailable--;
                var rental = new Rental
                                 {
                                     Customer = customer,
                                     Movie = movie,
                                     DateRented = DateTime.Now
                                 };

                this._context.Rentals.Add(rental);
            }

            this._context.SaveChanges();

            return this.Ok();
        }
    }
}

/*
 * Edge case
 * * CustomerId is invalid
 * * No MovieIds
 * * One or more MovieIds are invalid
 * * One or more movies are not available
 *
 */

