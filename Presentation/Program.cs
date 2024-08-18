using Datos;
using Presentation.Extensions;
using Servicio.Interface;
using Servicio.IOC;
using Servicio.Services.Service;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ISeedService, SeedService>();
            builder.Services.AddPersistenceInfrastructure(builder.Configuration);
            builder.Services.AddIdentityInfrastructureForApi(builder.Configuration);
            builder.Services.AddServicesPlayer();
            builder.Services.AddSwaggerExtension();
            builder.Services.AddApiVersioningExtension();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var seedService = scope.ServiceProvider.GetRequiredService<ISeedService>();
                try
                {
                    seedService.SeedAsync(scope.ServiceProvider);
                }
                catch (Exception ex)
                {
                    // Maneja la excepción según sea necesario
                }

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }


                app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
        }
    }
}
