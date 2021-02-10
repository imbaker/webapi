using AutoMapper;
using WebApi.Models.Dto;
using WebApi.Models.Domain;

namespace WebApi.Models.Mappers
{
    public class PolicyProfile : Profile
    {
        public PolicyProfile()
        {
            CreateMap<Policy, PolicyDto>().ReverseMap();

            CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.Postcode, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}