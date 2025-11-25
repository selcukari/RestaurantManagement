using AutoMapper;
using RestaurantManagement.Basket.Api.Data;
using RestaurantManagement.Basket.Api.Dto;

namespace RestaurantManagement.Basket.Api.Features
{
    public class BasketMapping: Profile
    {
        public BasketMapping()
        {
            CreateMap<BasketDto, Data.Basket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        }
    }
}
