using AutoMapper;
using NZWalksAPI.DTO;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Incase property name not same like below
            //CreateMap<UserDomain, UserDTO>()
            //.ForMember(x => x.MyName, opt => opt.MapFrom(t => t.Name)).ReverseMap();

            CreateMap<Region, RegionsDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
        }

    }
    public class UserDomain
    {
        public string  Name { get; set; }
    }
    public class UserDTO
    {
        public string MyName { get; set; }
    }
}
