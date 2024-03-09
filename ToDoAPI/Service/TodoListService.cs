using Domain.Interface;
using Domain.Models;
using ToDoAPI.Repository.IRepository;
using Utils.Shared.DTO;
using Utils.Shared.Helper;

namespace ToDoAPI.Service
{
    public class TodoListService : IToDoListService
    {
        private readonly IToDoListRepository _toDoListRepo;

        public TodoListService(IToDoListRepository toDoListRepo)
        {
            _toDoListRepo = toDoListRepo;
        } 
        public async Task<ToDoList> CreateToDoList(ToDoList todolist)
        {
            await _toDoListRepo.CreateAsync(todolist);
            todolist.Id = 0;
            return todolist;
        }
        public async Task<bool> DeleteToDoList(TodoTask entity)
        {
            try
            {
                _toDoListRepo.RemoveAsync(entity.Id);
               return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public async Task<List<ToDoList>> GetAllToDoList()
        {
            return  await _toDoListRepo.GetAllAsync(a=> a.IsActive == SD.IsActive);
        }

        public async Task<ToDoList> GetToDoListById(int id)
        {

            return await _toDoListRepo.GetFirstOrDefaultAsync(a => a.Id == id);
             
        }

        public async Task<bool> UpdateToDoList( ToDoListUpdateDTO updateDTO)
        {
            try
            {
                _toDoListRepo.UpdateToDoListAsync(updateDTO); 
                return true;
            }
            catch (Exception)
            {

                return false;
            }
          
        }
        public async Task<ToDoList> GetToDoListByUserId(int id)
        {

            return await _toDoListRepo.GetFirstOrDefaultAsync(a => a.UserId == id);

        }
    }
}
