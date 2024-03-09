using Domain.Models;
using Utils.Shared.DTO;

namespace ToDoAPI.Repository.IRepository
{
    public interface IToDoTaskRepository : IRepositoryAsync<TodoTask>
    { 
         void UpdateToDoTaskAsync(ToDoTaskUpdateDto entity);
         void DeleteToDoTaskAsync(int id);
    }
}
