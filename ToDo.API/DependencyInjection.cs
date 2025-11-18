using ToDo.Commands.ToDo;
using ToDo.Core.Contexts;

namespace ToDo.API
{
    public static class DependencyInjection
    {
        public static void AddAPI(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<ToDoDbContext>();
                cfg.RegisterServicesFromAssembly(typeof(CreateToDoCommand).Assembly);
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "ToDo API", Version = "v1" });
            });
        }
    }
}
