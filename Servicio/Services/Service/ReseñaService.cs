using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Servicio.Interface;
using Entidades.Entity;
using Datos;

namespace Servicio.Services.Service
{
    public class ReseñaService : IReseñaService
    {
        private readonly AppDbContext _context;

        private readonly ILogger<ReseñaService> _logger;

        public async Task<IEnumerable<Reseña>> GetAllReseñas()
        {
            return await _context.Reseñas.ToListAsync();
        }

        public async Task<IEnumerable<Reseña>> GetReseñasByProducts(int idProducto)
        {

            var query = _context.Reseñas.Where(r => r.IdProducto == idProducto);

            return await query.ToListAsync();
        }


        public async Task<bool> DeleteReseña(int idReseña)
        {
            var reseña = await _context.Reseñas.FindAsync(idReseña);

            if (reseña == null)
            {
                _logger.LogWarning($"Reseña con id {idReseña} no encontrado.");
                return false;
            }
            _context.Reseñas.Remove(reseña);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Reseña con id {idReseña} borrada existosamente");
            return true;
        }

        public async Task<Reseña> CreateReseña(Reseña newReseña)
        {
            _logger.LogInformation($"Registrando reseña {newReseña.Comentario}");
            if (newReseña.Comentario == null || newReseña.IdUsuario == null)
            {
                _logger.LogInformation($"La reseña que se ha intentado registrar no cumple con los datos necesarios");
                return null;
            }
            _context.Reseñas.Add(newReseña);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Producto creado exitosamente con id {newReseña.IdReseña} pertenece al Producto con id {newReseña}");
            return newReseña;
        }

    }
}
