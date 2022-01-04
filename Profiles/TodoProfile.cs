using AutoMapper;
using TodoService.Models;
using TodoService.Dtos;

namespace TodoService.profiles
{
    public class TodoProfile : Profile
    {
        
        public TodoProfile()
        {
            CreateMap<Todo, TodoReadDto>();
            CreateMap<TodoCreateDto, Todo>();
            CreateMap<TodoUpdateDto, Todo>();
            CreateMap<TodoStatusUpdateDto, Todo>();
        }
        
    }
}