using AutoMapper;
using Data.Entities;
using Domain.ApiModel.RequestApiModels;
using Domain.ApiModel.ResponseApiModels;

namespace Domain.Mapping
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<RegisterRequestApiModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName)).ReverseMap();
         
            CreateMap<PersonalInformationResponseApiModel, User>().ReverseMap();
        }
    }
}
