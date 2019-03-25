using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.ViewModels
{
    using Vidly.Models;

    public class RandomMovieViewModel
    {
        public Movie Movie {get; set; }

        public List<Customer> Customers { get; set; }
    }
}