using DataAccess.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class AddressRepository : IAddressRepository
    {
        private DataContext _context;
        public AddressRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateAddress(Address address)
        {
            _context.Addresses.Add(address);
        }

        public void DeleteAddress(Address address)
        {
            _context.Addresses.Remove(address);
        }

        public Address GetAddressById(int id)
        {
            return _context.Addresses.FirstOrDefault(address => address.Id.Equals(id));
        }

        public List<Address> GetAddressByUser(int Userid)
        {
            return _context.Addresses.Where(address => address.UserId.Equals(Userid)).ToList();
        }

        public List<Address> GetAllAddresses()
        {
            return _context.Addresses.ToList();
        }

        public void UpdateAddress(Address address)
        {
            _context.Addresses.Update(address);
        }
    }
}
