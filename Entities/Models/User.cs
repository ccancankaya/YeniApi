using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class User
    {
        public User()
        {
            Addresses = new List<Address>();
            Orders = new List<Order>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string PublicId { get; set; }
        public bool IsActive { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Order> Orders { get; set; }
        public Kitchen Kitchen { get; set; }
    }
}
