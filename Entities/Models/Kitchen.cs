using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Kitchen
    {
        public Kitchen()
        {
            Foods = new List<Food>();
            KitchenCategories = new List<KitchenCategory>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string KitchenName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string LogoUrl { get; set; }
        public string PublicId { get; set; }
        public string PayType { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public User User { get; set; }
        public List<Food> Foods { get; set; }
        public List<KitchenCategory> KitchenCategories { get; set; }
    }
}
