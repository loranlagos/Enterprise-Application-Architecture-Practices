using AutoMapper;
using Pacogroup.Ecommerce.Application.DTO;
using Pacogroup.Ecommerce.Domain.Entity;

namespace Pacogroup.Ecommerce.Application.Main.Commons
{
    public class MappingsProfile : Profile
    {
        protected MappingsProfile()
        {
            CreateMap<Costumer, CustomerDTO>().ReverseMap();
        }

        protected internal MappingsProfile(string profileName) : base(profileName)
        {
        }
    }
}