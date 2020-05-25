using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
