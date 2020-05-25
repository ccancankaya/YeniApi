using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;


namespace Entities.DataTransferObjects
{
    public class PhotoCreateDto
    {
        public PhotoCreateDto()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }
        public IFormFile File { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
