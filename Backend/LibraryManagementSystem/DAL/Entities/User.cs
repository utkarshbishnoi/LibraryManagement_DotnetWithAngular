using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User 
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        
        [Required]
        public int TokensAvailable { get; set; } 
        
        public List<Book> BorrowedBooks { get; set; }
        
        public virtual List<Book> LentBooks { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ratings> Ratings { get; set; }
    }
}
