using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICategoryRepository 
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
    }
}
