using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
    public class Book
    {
       
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Book name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Book author is required")]
        public string Author { get; set; }
        
        [Required(ErrorMessage = "Book genre is required")]
        public string Genre { get; set; }
        
        [Required(ErrorMessage = "Book description is required")]
        public string Description { get; set; }

        public bool IsBookAvailable { get; set; } = true;

        public int? LentByUserId { get; set; } 

        public int? CurrentlyBorrowedById { get; set; }

        [JsonIgnore]
        public virtual User LentByUser { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ratings> Ratings { get; set; }

    }
}
