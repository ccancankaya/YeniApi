using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class AddressDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string AddressLine { get; set; }
        public string AddressDetail { get; set; }
        public string Neighborhood { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public bool IsMain { get; set; }
    }
}
