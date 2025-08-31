using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace naidisprojekt.Models
{
    public class Listing
    {
        public int ListingId { get; set; }
        public decimal Price { get; set; }
        public string ListingName { get; set; }
        public string ListingDescription { get; set; }
        public byte[]? ListingImage { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
