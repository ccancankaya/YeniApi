using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class OrderCreateDto
    {
        public OrderCreateDto()
        {
            OrderDate = DateTime.Now;
            Status = "Hazırlanıyor";
        }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
        public string OrderDetails { get; set; }
    }
}
