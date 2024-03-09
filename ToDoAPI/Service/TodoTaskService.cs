using Domain.Interface;
using Domain.Models;
using ToDoAPI.Repository.IRepository;
using Utils.Shared.DTO;
using Utils.Shared.Helper;

namespace ToDoAPI.Service
{
    public class TodoTaskService : IToDoTaskService
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;

        public TodoTaskService(IToDoTaskRepository toDoTaskRepository)
        {
            _toDoTaskRepository = toDoTaskRepository;
        } 
        public async Task<TodoTask> CreateToDoTask(TodoTask todoTask)
        {
            await _toDoTaskRepository.CreateAsync(todoTask);
            todoTask.Id = 0;
            return todoTask;
        }
        public async Task<bool> DeleteToDoTask(int id)
        {
            try
            {
                await _toDoTaskRepository.RemoveAsync(id);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<TodoTask>> GetAllToDoTask()
        {
            return  await _toDoTaskRepository.GetAllAsync(a=> a.IsActive == SD.IsActive);
        }

        public async Task<TodoTask> GetToDoTaskById(int id)
        {

            return await _toDoTaskRepository.GetFirstOrDefaultAsync(a => a.Id == id);
             
        }

        public async Task<bool> UpdateToDoTask( ToDoTaskUpdateDto updateDTO)
        {
            try
            {
                _toDoTaskRepository.UpdateToDoTaskAsync(updateDTO); 
                return true;
            }
            catch (Exception)
            {

                return false;
            }
          
        }
    }
}
