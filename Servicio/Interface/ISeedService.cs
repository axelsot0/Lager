namespace Servicio.Interface
{
    public interface ISeedService
    {
        Task SeedAsync(IServiceProvider services);
    }
}