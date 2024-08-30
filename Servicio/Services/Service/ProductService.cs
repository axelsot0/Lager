using Entidades;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Servicio.Interface;
using Datos;
using Entidades.Entity;
using Entidades.Dtos.Entity;
using AutoMapper;


namespace Servicio.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        public ProductService(AppDbContext context, ILogger<ProductService> logger, IMapper mapper)
        {
            _mapper = mapper;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Producto>> GetAllProducts()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetFilteredProducts(FiltroProducts objFiltro)
        {
            _logger.LogInformation("Mostrando los productos con filtros (algunos pueden ser null)");

            var query = _context.Productos.AsQueryable();


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

        public async Task<Producto> CreateProduct(ProductDTO product)
        {
            _logger.LogInformation($"Vamos a crear un nuevo producto: {product.NombreProducto}");

            Producto newProduct = _mapper.Map<Producto>(product);
            
            _context.Productos.Add(newProduct);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Producto creado exitosamente con id {newProduct.IdProducto}");
            return newProduct;
        }

        public async Task<bool> EditProduct(int id, Producto updatedProduct)
        {
            _logger.LogInformation($"Vamos a editar el producto con id {id}");

            var product = await _context.Productos.FindAsync(id);

            if (product == null)
            {
                _logger.LogWarning($"Producto con id {id} no encontrado.");
                return false;
            }

            product.NombreProducto = updatedProduct.NombreProducto;
            product.Descripcion = updatedProduct.Descripcion;
            product.Precio = updatedProduct.Precio;
            product.Existencias = updatedProduct.Existencias;
            product.Tipo = updatedProduct.Tipo;
            product.Modelo = updatedProduct.Modelo;
            product.Precio = updatedProduct.Precio;

            product.Fotos = updatedProduct.Fotos;

            _context.Productos.Update(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Producto con id {id} editado exitosamente.");
            return true;
        }



        public async Task<bool> DeleteProduct(int id)
        {
            _logger.LogInformation($"Vamos a borrar el producto con id {id}");

            var product = await _context.Productos.FindAsync(id);
            if (product == null)
            {
                _logger.LogWarning($"Producto con id {id} no encontrado.");
                return false;
            }

            _context.Productos.Remove(product);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Producto con id {id} borrado exitosamente.");
            return true;
        }



    }

}

