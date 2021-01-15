using AutoMapper;
using jairoEcomerce.Data.Entities;
using jairoEcomerce.Pages;
using jairoEcomerce.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jairoEcomerce.Data
{
    public class jairoEcommerceMappingProfile : Profile
    {
        public jairoEcommerceMappingProfile()
        {
          CreateMap<Order, OrderViewModel>()
          .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
          .ReverseMap();
          CreateMap<OrderItem, OrderItemViewModel>()
          .ReverseMap();
            CreateMap<Product, ProductViewModel>()
            .ReverseMap();
        }
    }
}
