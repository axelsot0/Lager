using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Servicio.Services.Service
{
    public class FotoService
    {
        private readonly AppDbContext _context;

        private readonly ILogger<FotoService> _logger;


        public async Task<IEnumerable<Foto>> GetAllFotos()
        {
            return await _context.Fotos.ToListAsync();
        }

        public async Task<IEnumerable<Foto>> GetFotosById(int idProducto)
        {
            _logger.LogInformation($"Buscando fotos por producto");


            var query = _context.Fotos.AsQueryable();

            query = query.Where(f => f.IdProducto.Contains(idProducto));
            if (query == null)
            {
                return null; 
            }
            return query.ToListAsync();
        }   

        public async Task<Foto> AddFoto(Foto foto)
        {
            _logger.LogInformation($"Agregando foto a producto");
            _context.Fotos.Add(foto);
            await _context.SaveChangesAsync();
            int id = foto.IdFoto;
            int idP = foto.IdProducto;

            var ft = _context.FindAsync(id);
            if (ft == null)
            {
                _logger.LogWarning("No se encontró la foto guardada, error a la hora de guardar");
                return null;
            }
            _logger.LogInformation($"Foto con id {ft.IdFoto} perteneciente a producto con id {ft.IdProducto} ha sido registrada exitosamente");
            return ft;
        }

        public async Task<bool> DeleteFoto(Foto foto)
        {
            _logger.LogInformation($"Vamos a borrar la foto con id{foto.IdFoto}");

            var foto = await _context.Fotos.FindAsync(foto.IdFoto);

            if(foto == null)
            {
                _logger.LogWarning($"Foto con id {foto.idFoto} no encontrado.");
                return false;
            }

            _context.Productos.Remove(foto);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Foto con id {foto.id} borrado exitosamente.");
            return true;
        }
    }
}
