using Domain.Models;
using Infrastructure.Data;
using ToDoAPI.Repository.IRepository;
using Utils.Shared.DTO;
using Utils.Shared.Enums;

namespace ToDoAPI.Repository
{
    public class ToDoListRepository :  RepositoryAsync<ToDoList>, IToDoListRepository
    {
          private readonly ApplicationDbContext _db;

        public ToDoListRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
         

        public void DeleteToDoListAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateToDoListAsync(ToDoListUpdateDTO entity)
        {
            var objFromDb = _db.ToDoLists.FirstOrDefault(a => a.Id == entity.Id);
            if (objFromDb != null)
            {   
                objFromDb.Name = entity.Name;
                objFromDb.Description = entity.Description; 

                //_db.Restaurants.Update(objFromDb);
                //await _db.SaveChangesAsync();
            }
        }
    }
}
