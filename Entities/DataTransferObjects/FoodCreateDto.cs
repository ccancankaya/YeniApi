using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class FoodCreateDto
    {
        public FoodCreateDto()
        {
            IsActive = true;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }
        public int KitchenId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
