using DataAccess.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        private DataContext _context;
        public OrderRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            return _context.Orders.Where(order => order.UserId.Equals(userId)).ToList();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}
