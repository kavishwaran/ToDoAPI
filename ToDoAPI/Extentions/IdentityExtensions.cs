using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace ToDoAPI.Extentions
{
    public static class IdentityExtensions
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

    }
}
