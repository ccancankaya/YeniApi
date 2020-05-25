using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserRepository 
    {
        List<User> GetAllUsers();
        User GetById(int id);
        User GetByEmail(string email);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        void UpdateDescription(User user);

    }
}
