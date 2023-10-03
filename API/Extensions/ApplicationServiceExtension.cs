
using API.Service;
using Aplicacion.UnitOfWork;
using Dominio.Interfaces;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
        }
        public static void ConfigureCors(this IServiceCollection services) => services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", builder => 
            {
                builder.AllowAnyHeader()
                       .AllowAnyOrigin()
                       .AllowAnyMethod();  
            });
        });
    }
}