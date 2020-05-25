using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IKitchenRepository 
    {
        PagedList<Kitchen> GetAllKitchens(PageParameters pageParameters);
        Kitchen GetKitchenById(int id);
        Kitchen GetKitchenByName(string Name);
        Kitchen GetKitchenByUser(int Userid);
        void CreateKitchen(Kitchen kitchen);
        void UpdateKitchen(Kitchen kitchen);
        void DeleteKitchen(Kitchen kitchen);
    }
}
