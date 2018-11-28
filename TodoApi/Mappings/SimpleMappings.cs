using AutoMapper;
using TaskApi.DTO;
using TaskApi.Models;
using TodoApi.DTO;

namespace TodoApi.Mappings
{
    public class SimpleMappings : Profile
        {
        public SimpleMappings()
        {
            //CreateMap<TaskItem, CreateTaskDTO>().ReverseMap();
            CreateMap<CreateTaskDTO, TaskItem>();
            CreateMap<UpdateTaskDTO, TaskItem>();
            CreateMap<TaskItem, TaskDTO>();
        }
    }
}