using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class KitchenDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string KitchenName { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
