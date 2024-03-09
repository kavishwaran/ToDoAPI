using Domain.Models;
using Infrastructure.Data;
using ToDoAPI.Repository.IRepository;

namespace ToDoAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(db);
            ToDoList = new ToDoListRepository(db);
            ToDoTask = new ToDoTaskRepository(db);
        }
        public IUserRepository User { get; private set; } 
        public IToDoListRepository ToDoList { get; private set; } 
        public IToDoTaskRepository ToDoTask { get; private set; } 
        // public IEmergencyAlertRepository emergencyAlert => throw new NotImplementedException();

        public void Dispose()
        {
            _db.Dispose();
        }

        public bool Save()
        {
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

    }
}
