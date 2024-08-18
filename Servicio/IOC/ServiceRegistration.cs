using Microsoft.Extensions.DependencyInjection;
using Servicio.Interface;
using Servicio.Services;
using Servicio.Services.Account;
using Servicio.Services.Service;
using Microsoft.Extensions.DependencyInjection;


namespace Servicio.IOC
{
    public static class ServiceRegistration
    {
        public static void AddServicesPlayer(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IFotoService, FotoService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IReseñaService, ReseñaService>();
            services.AddTransient<ICompraService, CompraService>();
        }
    }
}
