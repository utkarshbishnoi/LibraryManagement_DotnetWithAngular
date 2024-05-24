using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.DTO
{
    public class UpdateBookDTO
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsBookAvailable { get; set; }
        public string Description { get; set; }
        public int? LentByUserId { get; set; } = 0;
        public int? CurrentlyBorrowedById { get; set; }
    }
}
