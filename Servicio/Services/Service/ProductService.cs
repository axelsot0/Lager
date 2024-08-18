using Datos;
using Entidades.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Servicio.Services.Service
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        private readonly ILogger<ProductService> _logger;
        //hola
        public async Task<List<Producto>> GetAllProducts()
        {
            return await _context.Productos.ToListAsync();
        }

    }
    public class Filtro
    {
        public string? NombreProducto { get; set; }
        public string? Marca { get; set; }
        public string? Tipo { get; set; }
        public string? Modelo { get; set; }
        public float? PrecioMin { get; set; }
        public float? PrecioMax { get; set; }
    }
}
