using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            //List Dtos
            CreateMap<User, UserDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Food, FoodDto>().ForMember(dest => dest.PhotoUrl, opt =>
               {
                   opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
               });
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<KitchenCategory, KitchenCategoryDto>();
            CreateMap<Kitchen, KitchenDto>();
            CreateMap<Kitchen, KitchenDetailDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Photo, PhotoDto>();
            CreateMap<Food, FoodDetailDto>();

            //Create Dtos
            CreateMap<UserCreateDto, User>();
            CreateMap<AddressCreateDto, Address>();
            CreateMap<FoodCreateDto, Food>();
            CreateMap<InvoiceCreateDto, Invoice>();
            CreateMap<KitchenCategoryCreateDto, KitchenCategory>();
            CreateMap<KitchenCreateDto, Kitchen>();
            CreateMap<OrderCreateDto, Order>();
            CreateMap<PhotoCreateDto, Photo>();
            CreateMap<UserRegisterDto, User>();


            //Update Dtos
            CreateMap<AddressUpdateDto, Address>();
            CreateMap<FoodUpdateDto, Food>();
            CreateMap<KitchenCategoryUpdateDto, KitchenCategoryDto>();
            CreateMap<KitchenUpdateDto, Kitchen>();
            CreateMap<UserUpdateDto, User>();

        }
    }
}
