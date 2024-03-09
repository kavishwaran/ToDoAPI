using AutoMapper;
using Domain.Models;
using Utils.Shared.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoAPI.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ApplicationUser, AdministrationUserDTO>().ReverseMap();

            CreateMap<User, UserCreateDto>();
            CreateMap<User, UserCreateDto>().ReverseMap();


            CreateMap<User, UserUpdateDto>();
            CreateMap<User, UserUpdateDto>().ReverseMap(); 
            
            CreateMap<ToDoList, ToDoListCreateDTO>();
            CreateMap<ToDoList, ToDoList>().ReverseMap();


            CreateMap<ToDoList, ToDoListUpdateDTO>();
            CreateMap<ToDoList, ToDoListUpdateDTO>().ReverseMap();

            CreateMap<TodoTask, ToDoTaskCreateDto>();
            CreateMap<TodoTask, ToDoTaskCreateDto>().ReverseMap(); 

            CreateMap<TodoTask, ToDoTaskUpdateDto>();
            CreateMap<TodoTask, ToDoTaskUpdateDto>().ReverseMap();
            //CreateMap<Restaurant, RestaurantUpdateDTO>();
            //CreateMap<Restaurant, RestaurantUpdateDTO>().ReverseMap();

        }

    }
}
