using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class KitchenCategory
    {
        public int Id { get; set; }
        public int KitchenId { get; set; }
        public int CategoryId { get; set; }
        public Kitchen Kitchen { get; set; }
        public Category Category { get; set; }
    }
}
