using Domain.Models;
using Utils.Shared.DTO;

namespace Domain.Interface
{
    public interface IToDoListService
    {
        Task<ToDoList> GetToDoListById(int id);
        Task <List<ToDoList>> GetAllToDoList();
       // Task<User> CreateRestaurant(User user);
        Task<ToDoList> CreateToDoList(ToDoList User);
        Task<bool> UpdateToDoList(ToDoListUpdateDTO user);
        Task<bool> DeleteToDoList(int id);
        Task<ToDoList> GetToDoListByUserId(int id);
    }
}
