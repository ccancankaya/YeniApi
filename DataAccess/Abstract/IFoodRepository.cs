using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IFoodRepository 
    {
        PagedList<Food> GetAllFoods(PageParameters pageParameters);
        Food GetFoodById(int id);
        int GetLatestFoodId();
        PagedList<Food> GetFoodsByCategoryId(int CategoryId, PageParameters pageParameters);
        PagedList<Food> GetFoodsByKitchenId(int KitchenId, PageParameters pageParameters);
        void CreateFood(Food food);
        void UpdateFood(Food food);
        void DeleteFood(Food food);


    }
}
