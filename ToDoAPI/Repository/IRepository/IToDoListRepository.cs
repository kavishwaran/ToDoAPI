using Domain.Models;
using Utils.Shared.DTO;

namespace ToDoAPI.Repository.IRepository
{
    public interface IToDoListRepository : IRepositoryAsync<ToDoList>
    { 
         void UpdateToDoListAsync(ToDoListUpdateDTO entity);
         void DeleteToDoListAsync(int id);
    }
}
