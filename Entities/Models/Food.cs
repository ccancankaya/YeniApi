using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Food
    {
        public Food()
        {
            Photos = new List<Photo>();
        }
        public int Id { get; set; }
        public int KitchenId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Kitchen Kitchen { get; set; }
        public List<Photo> Photos { get; set; }
        public Category Category { get; set; }
        //public string PhotoUrl { get; set; }
    }
}
