
using Datos;
using Entidades;
using Microsoft.AspNetCore.Identity;
using Servicio.Interface;
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var seedService = scope.ServiceProvider.GetRequiredService<ISeedService>();
                try
                {
                    seedService.SeedAsync();
                }
                catch (Exception ex)
                {
                    
                }
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
