using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Core.Contexts;
using ToDo.Core.Services;

namespace ToDo.Core
{
    public static class DependencyInjection
    {
        public static void AddCore(this IServiceCollection services)
        {
            services.AddDbContext<ToDoDbContext>(options =>
                options.UseInMemoryDatabase("ToDoDb"));

            services.AddAutoMapper(typeof(ToDoDbContext).Assembly);

            services.AddScoped<IToDoService, ToDoService>();
        }
    }
}
