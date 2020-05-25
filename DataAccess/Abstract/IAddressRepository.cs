using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IAddressRepository
    {
        List<Address> GetAllAddresses();
        Address GetAddressById(int id);
        List<Address> GetAddressByUser(int Userid);
        void CreateAddress(Address address);
        void UpdateAddress(Address address);
        void DeleteAddress(Address address);
    }
}
