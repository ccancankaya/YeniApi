using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DataContext _context;
        public RepositoryWrapper(DataContext context)
        {
            _context = context;
        }
        private IAddressRepository _address;

        private ICategoryRepository _category;

        private IFoodRepository _food;

        private IInvoiceRepository _ınvoice;

        private IOrderRepository _order;

        private IKitchenCategoryRepository _kitchenCategory;

        private IKitchenRepository _kitchen;

        private IPhotoRepository _photo;

        private IUserRepository _user;
        private IAuthRepository _auth;



        public IAddressRepository Address
        {
            get
            {
                if (_address == null)
                {
                    _address = new AddressRepository(_context);
                }

                return _address;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_context);
                }

                return _category;
            }
        }

        public IFoodRepository Food
        {
            get
            {
                if (_food == null)
                {
                    _food = new FoodRepository(_context);
                }

                return _food;
            }
        }

        public IInvoiceRepository Invoice
        {
            get
            {
                if (_ınvoice == null)
                {
                    _ınvoice = new InvoiceRepository(_context);
                }

                return _ınvoice;
            }
        }

        public IOrderRepository Order
        {
            get
            {
                if (_order == null)
                {
                    _order = new OrderRepository(_context);
                }

                return _order;
            }
        }

        public IKitchenCategoryRepository KitchenCategory
        {
            get
            {
                if (_kitchenCategory == null)
                {
                    _kitchenCategory = new KitchenCategoryRepository(_context);
                }

                return _kitchenCategory;
            }
        }

        public IKitchenRepository Kitchen
        {
            get
            {
                if (_kitchen == null)
                {
                    _kitchen = new KitchenRepository(_context);
                }

                return _kitchen;
            }
        }

        public IPhotoRepository Photo
        {
            get
            {
                if (_photo == null)
                {
                    _photo = new PhotoRepository(_context);
                }

                return _photo;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }

                return _user;
            }
        }

        public IAuthRepository Auth
        {
            get
            {
                if (_auth == null)
                {
                    _auth = new AuthRepository(_context);
                }

                return _auth;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
