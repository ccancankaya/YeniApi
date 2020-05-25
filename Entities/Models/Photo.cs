using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public bool IsMain { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public Food Food { get; set; }
    }
}
