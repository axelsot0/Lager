using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Contexts;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Servicio.Interface;
using Datos;
using Entidades.Entity;

namespace Servicio.Services.Service
{
    public class ProductService : IProductService
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

        public async Task<Producto> CreateProduct(Producto newProduct)
        {
            _logger.LogInformation($"Vamos a crear un nuevo producto: {newProduct.NombreProducto}");

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
            product.Foto = updatedProduct.Foto;
            product.Precio = updatedProduct.Precio;

            _context.Productos.Update(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Producto con id {id} editado exitosamente.");
            return true;
        }



        public async Task<bool> DeleteProduct(int id)
        {
            _logger.LogInformation($"Vamos a borrar el producto con id{id}");

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

