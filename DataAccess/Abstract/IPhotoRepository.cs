using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IPhotoRepository 
    {
        List<Photo> GetPhotosByFoodId(int Foodid);
        Photo GetPhotoById(int id);

        void CreatePhoto(Photo photo);
        void UpdatePhoto(Photo photo);
        void DeletePhoto(Photo photo);
    }
}
