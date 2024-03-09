using Domain.Models;
using Infrastructure.Data;
using ToDoAPI.Repository.IRepository;
using Utils.Shared.DTO;
using Utils.Shared.Enums;

namespace ToDoAPI.Repository
{
    public class UserRepository :  RepositoryAsync<User>, IUserRepository
    {
          private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void CreateUserAsync(UserCreateDto entity)
        {
            
            throw new NotImplementedException();
        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(UserUpdateDto entity)
        {
            var objFromDb = _db.Users.FirstOrDefault(a => a.Id == entity.Id);
            if (objFromDb != null)
            {  
                 objFromDb.Id = entity.Id;
                objFromDb.FirstName = entity.FirstName;
                objFromDb.LastName = entity.LastName;
                objFromDb.Email = entity.Email;
                objFromDb.GenderEn = entity.GenderEn; 

                //_db.Restaurants.Update(objFromDb);
                //await _db.SaveChangesAsync();
            }
        }
    }
}
