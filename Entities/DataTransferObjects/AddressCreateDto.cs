﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class AddressCreateDto
    {
        public AddressCreateDto()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
            IsMain = true;
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string AddressLine { get; set; }
        public string AddressDetail { get; set; }
        public string Neighborhood { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public bool IsMain { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
