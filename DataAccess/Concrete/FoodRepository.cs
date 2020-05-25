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
    public class FoodRepository : IFoodRepository
    {
        private DataContext _context;
        public FoodRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateFood(Food food)
        {
            _context.Foods.Add(food);
        }

        public void DeleteFood(Food food)
        {
            _context.Foods.Remove(food);
        }

        public PagedList<Food> GetAllFoods(PageParameters pageParameters)
        {
            return PagedList<Food>.ToPagedList(_context.Foods.Include(food => food.Photos).OrderBy(n => n.Name).ToList(), pageParameters.PageNumber, pageParameters.PageSize);
        }

        public Food GetFoodById(int id)
        {
            return _context.Foods.Include(food => food.Photos).FirstOrDefault(food => food.Id.Equals(id));
        }

        public PagedList<Food> GetFoodsByCategoryId(int CategoryId, PageParameters pageParameters)
        {
            return PagedList<Food>.ToPagedList(_context.Foods.Include(food => food.Photos).Where(food => food.CategoryId.Equals(CategoryId) || food.CategoryId == 0).ToList(), pageParameters.PageNumber, pageParameters.PageSize);
        }

        public PagedList<Food> GetFoodsByKitchenId(int KitchenId, PageParameters pageParameters)
        {
            return PagedList<Food>.ToPagedList(_context.Foods.Include(food => food.Photos).Where(food => food.KitchenId.Equals(KitchenId)).ToList(), pageParameters.PageNumber, pageParameters.PageSize);
        }

        public int GetLatestFoodId()
        {
            return _context.Foods.AsEnumerable().LastOrDefault().Id;
        }

        public void UpdateFood(Food food)
        {
            _context.Foods.Update(food);
        }
    }
}
