using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Servicio.Services.Service
{
    public class CompraService
    {
        private readonly AppDbContext _context;

        private readonly ILogger<CompraService> _logger;

        public async Task<IEnumerable<Compra>> GetAllCompras()
        {
            return await _context.Compras
        }

        public async Task<IEnumerable<Compra>> GetComprasByUser(int idUser)
        {
            var query = _context.Compras.AsQueryable();

            query = query.Where(c => c.IdUsuario == idUser);
            return await query.ToListAsync();
        }

        public async Task<Compra> CreateCompra(Compra newCompra)
        {
            _context.Compras.Add(newCompra);
            await _context.SaveChangesAsync();
            return newProduct;
        }
        public async Task<bool> DeleteCompra(int id)
        {
            var compra = _context.Compras.FindAsync(id);

            if (compra == null)
            {
                _logger.LogInformation($"Compra con id {id} no encontrada");
                return false;
            }

            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();

            
            return true;
        }

    }
}
