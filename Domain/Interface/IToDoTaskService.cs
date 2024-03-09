using Domain.Models;
using Utils.Shared.DTO;

namespace Domain.Interface
{
    public interface IToDoTaskService
    {
        Task<TodoTask> GetToDoTaskById(int id);
        Task <List<TodoTask>> GetAllToDoTask();
       // Task<User> CreateRestaurant(User user);
        Task<TodoTask> CreateToDoTask(TodoTask todoTask);
        Task<bool> UpdateToDoTask(ToDoTaskUpdateDto update);
        Task<bool> DeleteToDoTask(int id);
    }
}
