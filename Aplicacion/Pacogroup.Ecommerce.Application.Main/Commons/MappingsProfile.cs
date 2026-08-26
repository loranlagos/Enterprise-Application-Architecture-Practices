using AutoMapper;
using Pacogroup.Ecommerce.Application.DTO;
using Pacogroup.Ecommerce.Domain.Entity;

namespace Pacogroup.Ecommerce.Application.Main.Commons
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Costumer, CustomerDTO>().ReverseMap();
        }
    }
}