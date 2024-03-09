using Domain.Models;
using Utils.Shared.DTO;

namespace Domain.Interface
{
    public interface IUserService
    {
        Task<User> GetUsersById(int id);
        Task <List<User>> GetAllUsers();
       // Task<User> CreateRestaurant(User user);
        Task<User> CreateUser(User User);
        Task<bool> UpdateUser(UserUpdateDto user);
        Task<bool> DeleteRestaurant(int id);
    }
}
