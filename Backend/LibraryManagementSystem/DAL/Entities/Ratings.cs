using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace DAL.Entities
{
    public class Ratings
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "BookId is required")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        public int Rating { get; set; }

        [JsonIgnore]
        public virtual Book Book { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
