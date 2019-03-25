using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Genre
    {
        public byte Id { get; set; }
        

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

    }
}