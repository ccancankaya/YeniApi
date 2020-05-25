using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class KitchenCreateDto
    {
        public KitchenCreateDto()
        {
            IsActive = true;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
            
        }
        public int UserId { get; set; }
        public string KitchenName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string LogoUrl { get; set; }
        public string PublicId { get; set; }
        public string PayType { get; set; }
        public IFormFile File { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
