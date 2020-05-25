using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IKitchenCategoryRepository 
    {
        List<KitchenCategory> GetKitchenCategoriesByKitchenId(int kitchenId);
        void CreateKitchenCategory(KitchenCategory kitchenCategory);
        void UpdateKitchenCategory(KitchenCategory kitchenCategory);
        void DeleteKitchenCategory(KitchenCategory kitchenCategory);

    }
}
