using Microsoft.Extensions.DependencyInjection;
using Servicio.Interface;
using Servicio.Services;
using Servicio.Services.Account;
using Servicio.Services.Service;
using Microsoft.Extensions.DependencyInjection;
using Entidades.Settings;
using Microsoft.Extensions.Configuration;
using Datos;
using Microsoft.EntityFrameworkCore;
using System.Reflection;



namespace Servicio.IOC
{
    public static class ServiceRegistration
    {
        public static void AddServicesPlayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IFotoService, FotoService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IReseñaService, ReseñaService>();
            services.AddTransient<ICompraService, CompraService>();
        }
    }
}
