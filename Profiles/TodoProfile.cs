using AutoMapper;
using todo_service_refwebsoftware.Models;
using todo_service_refwebsoftware.Dtos;

namespace todo_service_refwebsoftware.profiles
{
    public class TodoProfile : Profile
    {
        
        public TodoProfile()
        {
            CreateMap<Todo, TodoReadDto>();
            CreateMap<TodoCreateDto, Todo>();
            CreateMap<TodoUpdateDto, Todo>();
            CreateMap<TodoStatusUpdateDto, Todo>();
            CreateMap<Project, ProjectReadDTO>();
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<Specialization, SpecializationReadDto>();
            CreateMap<SpecializationCreateDto, Specialization>();

            // RabbitMQ
            CreateMap<Specialization, UpdateSpecializationAsyncDTO>();
            CreateMap<UpdateSpecializationAsyncDTO, Specialization>();
        }
        
    }
}