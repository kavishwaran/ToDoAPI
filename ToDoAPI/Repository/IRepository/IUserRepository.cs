using Domain.Models;
using Utils.Shared.DTO;

namespace ToDoAPI.Repository.IRepository
{
    public interface IUserRepository : IRepositoryAsync<User>
    {
         void CreateUserAsync(UserCreateDto entity);
         void UpdateAsync(UserUpdateDto entity);
         void DeleteAsync(int id);
    }
}
