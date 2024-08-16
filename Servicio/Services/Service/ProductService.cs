using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Contexts;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Servicio.Services.Service
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        private readonly ILogger<ProductService> _logger;
        
        public async Task<IEnumerable<Producto>> GetAllProducts()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetFilteredProducts(Filtro objFiltro)
        {
            _logger.LogInformation("Mostrando los productos con filtros (algunos pueden ser null)");

            // Inicia la consulta a partir del conjunto de productos
            var query = _context.Productos.AsQueryable();

            // Aplica los filtros opcionales solo si tienen valor
            if (!string.IsNullOrEmpty(objFiltro.NombreProducto))
            {
                query = query.Where(p => p.NombreProducto.Contains(objFiltro.NombreProducto));
            }

            if (!string.IsNullOrEmpty(objFiltro.Marca))
            {
                query = query.Where(p => p.Marca.Contains(objFiltro.Marca));
            }


            if (!string.IsNullOrEmpty(objFiltro.Modelo))
            {
                query = query.Where(p => p.Modelo.Contains(objFiltro.Modelo));
            }

            if (objFiltro.PrecioMin.HasValue)
            {
                query = query.Where(p => p.Precio >= objFiltro.PrecioMin.Value);
            }

            if (objFiltro.PrecioMax.HasValue)
            {
                query = query.Where(p => p.Precio <= objFiltro.PrecioMax.Value);
            }

            
            return await query.ToListAsync();
        }


    }
    public class Filtro
    {
        public string? NombreProducto { get; set; } 
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public float? PrecioMin { get; set; }
        public float? PrecioMax { get; set; }
    }
}
