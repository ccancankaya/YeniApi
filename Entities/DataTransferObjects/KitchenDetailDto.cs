using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class KitchenDetailDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string KitchenName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string LogoUrl { get; set; }
        public string PublicId { get; set; }
        public string PayType { get; set; }

    }
}
