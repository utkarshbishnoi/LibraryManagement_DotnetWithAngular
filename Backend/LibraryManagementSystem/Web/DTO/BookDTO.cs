using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.DTO
{
    public class BookDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public bool IsBookAvailable { get; set; } 
        [Required]
        public string Description { get; set; }

        public int LentByUserId { get; set; } = 0;
        public int CurrentlyBorrowedById { get; set; }
    }
}
