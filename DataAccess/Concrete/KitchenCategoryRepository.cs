using DataAccess.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class KitchenCategoryRepository : IKitchenCategoryRepository
    {
        private DataContext _context;
        public KitchenCategoryRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateKitchenCategory(KitchenCategory kitchenCategory)
        {
            _context.KitchenCategories.Add(kitchenCategory);
        }

        public void DeleteKitchenCategory(KitchenCategory kitchenCategory)
        {
            _context.KitchenCategories.Remove(kitchenCategory);
        }

        public List<KitchenCategory> GetKitchenCategoriesByKitchenId(int kitchenId)
        {
            return _context.KitchenCategories.Where(kc => kc.KitchenId.Equals(kitchenId)).ToList();
        }

        public void UpdateKitchenCategory(KitchenCategory kitchenCategory)
        {
            _context.KitchenCategories.Update(kitchenCategory);
        }
    }
}
