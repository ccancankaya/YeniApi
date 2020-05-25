using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
        public string OrderDetails { get; set; }
        public User User { get; set; }

    }
}
