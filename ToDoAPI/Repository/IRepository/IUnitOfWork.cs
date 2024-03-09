namespace ToDoAPI.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; } 
        IToDoListRepository ToDoList { get; } 
        IToDoTaskRepository ToDoTask { get; } 
        bool Save();
    }
}
