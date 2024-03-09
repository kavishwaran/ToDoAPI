using Domain.Interface;
using Domain.Models;
using ToDoAPI.Repository.IRepository;
using Utils.Shared.DTO;
using Utils.Shared.Helper;

namespace ToDoAPI.Service
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        //public async Task<User> CreateRestaurant(User restaurant)
        //{ 
        //    await _restaurantRepository.CreateAsync(restaurant);
        //    restaurant.Id = 0;
        //    return restaurant;
        //}
        public async Task<User> CreateUser(User user)
        {
            await _userRepo.CreateAsync(user);
            user.Id = 0;
            return user;
        }
        public async Task<bool> DeleteRestaurant(int id)
        {
            try
            {
                _userRepo.RemoveAsync(id);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            return  await _userRepo.GetAllAsync(a=> a.IsActive == SD.IsActive);
        }

        public async Task<User> GetUsersById(int id)
        {

            return await _userRepo.GetFirstOrDefaultAsync(a => a.Id == id);
             
        }

        public async Task<bool> UpdateUser( UserUpdateDto updateDTO)
        {
            try
            {
                _userRepo.UpdateAsync(updateDTO); 
                return true;
            }
            catch (Exception)
            {

                return false;
            }
          
        }
    }
}
