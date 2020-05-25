using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRepositoryWrapper
    {
        IAddressRepository Address { get; }
        ICategoryRepository Category { get; }
        IFoodRepository Food { get; }
        IInvoiceRepository Invoice { get; }
        IOrderRepository Order { get; }
        IKitchenCategoryRepository KitchenCategory { get; }
        IKitchenRepository Kitchen { get; }
        IPhotoRepository Photo { get; }
        IUserRepository User { get; }
        IAuthRepository Auth { get; }

        void Save();

    }
}
