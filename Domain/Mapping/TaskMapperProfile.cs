using AutoMapper;
using Data.Entities;
using Domain.ApiModel.ResponseApiModels;

namespace Domain.Mapping
{
    public class TaskMapperProfile : Profile
    {
        public TaskMapperProfile()
        {
            CreateMap<UserTaskResponseApiModel, UserTask>().ReverseMap();
        }
    }
}
