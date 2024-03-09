using Domain.Interface;
using ToDoAPI.Repository;
using ToDoAPI.Repository.IRepository;
using ToDoAPI.Service;

namespace ToDoAPI.Extentions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {  
            services.AddScoped<IUserRepository, UserRepository>(); 
         //  services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IUse, RestaurantRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IToDoListRepository, ToDoListRepository>(); 

            services.AddScoped<IToDoListService, TodoListService>();



            services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();

            services.AddScoped<IToDoTaskService, TodoTaskService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAministrationUserRepository, AdministrationUserRepository>();

        }
    }
}
