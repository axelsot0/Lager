using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Servicio.Interface;
using Entidades.Entity;
using Datos;

namespace Servicio.Services.Service
{
    public class FotoService : IFotoService
    {
        private readonly AppDbContext _context;

        private readonly ILogger<FotoService> _logger;


        public async Task<IEnumerable<Foto>> GetAllFotos()
        {
            return await _context.Fotos.ToListAsync();
        }

        public async Task<IEnumerable<Foto>> GetFotosById(int idProducto)
        {
            _logger.LogInformation($"Buscando fotos por Producto");

            var query = _context.Fotos.AsQueryable();

            query = query.Where(f => f.IdProducto == idProducto);

            if (query == null)
            {
                return null;
            }
            return await query.ToListAsync();
        }

        public async Task<Foto> AddFoto(Foto foto)
        {
            _logger.LogInformation($"Agregando foto a Producto");
            _context.Fotos.Add(foto);
            await _context.SaveChangesAsync();
            int idP = foto.IdProducto;

            var ft = await _context.Fotos.FindAsync(foto.Id);
            if (ft == null)
            {
                _logger.LogWarning("No se encontró la foto guardada, error a la hora de guardar");
                return null;
            }
            _logger.LogInformation($"Foto con id {ft.Id} perteneciente a Producto con id {ft.IdProducto} ha sido registrada exitosamente");
            return ft;
        }

        public async Task<bool> DeleteFoto(int id)
        {
            _logger.LogInformation($"Vamos a borrar la foto con id{id}");

            var foto = await _context.Fotos.FindAsync(id);

            if (foto == null)
            {
                _logger.LogWarning($"Foto con id {id} no encontrado.");
                return false;
            }

            _context.Fotos.Remove(foto);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Foto con id {foto.Id} borrado exitosamente.");
            return true;
        }
    }
}
