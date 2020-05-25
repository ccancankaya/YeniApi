using DataAccess.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class PhotoRepository : IPhotoRepository
    {
        private DataContext _context;
        public PhotoRepository(DataContext context)
        {
            _context = context;
        }
        public void CreatePhoto(Photo photo)
        {
            _context.Photos.Add(photo);
        }

        public void DeletePhoto(Photo photo)
        {
            _context.Photos.Remove(photo);
        }

        public Photo GetPhotoById(int id)
        {
            return _context.Photos.FirstOrDefault(photo => photo.Id.Equals(id));
        }

        public List<Photo> GetPhotosByFoodId(int Foodid)
        {
            return _context.Photos.Where(photo => photo.FoodId.Equals(Foodid)).ToList();
        }

        public void UpdatePhoto(Photo photo)
        {
            _context.Photos.Update(photo);
        }
    }
}
