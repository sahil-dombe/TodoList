using AutoMapper;
using TodoList.Server.Models;
using TodoList.Shared.Dto;

namespace TodoList.Server.Data.Profiles
{
    public class ListOfTodosProfile : Profile
    {
        public ListOfTodosProfile()
        {
            CreateMap<ListOfTodos, ListOfTodosDto>();
            CreateMap<ListOfTodosForCreationDto, ListOfTodos>(); 
            CreateMap<ListOfTodosForUpdateDto, ListOfTodos>();
            CreateMap<ListOfTodos, ListOfTodosForUpdateDto>();
        }
    }
}
