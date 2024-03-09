using Domain.Models;
using Infrastructure.Data;
using ToDoAPI.Repository.IRepository;
using Utils.Shared.DTO;
using Utils.Shared.Enums;

namespace ToDoAPI.Repository
{
    public class ToDoTaskRepository :  RepositoryAsync<TodoTask>, IToDoTaskRepository
    {
          private readonly ApplicationDbContext _db;

        public ToDoTaskRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
         

        public void DeleteToDoTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateToDoTaskAsync(ToDoTaskUpdateDto entity)
        {
            var objFromDb = _db.TodoTasks.FirstOrDefault(a => a.Id == entity.Id);
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
