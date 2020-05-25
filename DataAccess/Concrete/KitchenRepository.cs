using DataAccess.Abstract;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class KitchenRepository : IKitchenRepository
    {
        private DataContext _context;
        public KitchenRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateKitchen(Kitchen kitchen)
        {
            _context.Kitchens.Add(kitchen);
        }

        public void DeleteKitchen(Kitchen kitchen)
        {
            _context.Kitchens.Remove(kitchen);
        }

        public PagedList<Kitchen> GetAllKitchens(PageParameters pageParameters)
        {
            return PagedList<Kitchen>.ToPagedList(_context.Kitchens.ToList(), pageParameters.PageNumber, pageParameters.PageSize);
        }

        public Kitchen GetKitchenById(int id)
        {
            return _context.Kitchens.FirstOrDefault(kitchen => kitchen.Id.Equals(id));
        }

        public Kitchen GetKitchenByName(string Name)
        {
            return _context.Kitchens.FirstOrDefault(kitchen => kitchen.KitchenName.Equals(Name));
        }

        public Kitchen GetKitchenByUser(int Userid)
        {
            return _context.Kitchens.FirstOrDefault(kitchen => kitchen.UserId.Equals(Userid));
        }

        public void UpdateKitchen(Kitchen kitchen)
        {
            _context.Kitchens.Update(kitchen);
        }
    }
}
