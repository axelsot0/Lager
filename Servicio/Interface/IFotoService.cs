using Entidades;

namespace Servicio.Interface
{
    public interface IFotoService
    {
        Task<Foto> AddFoto(Foto foto);
        Task<bool> DeleteFoto(int id);
        Task<IEnumerable<Foto>> GetAllFotos();
        Task<IEnumerable<Foto>> GetFotosById(int idProducto);
    }
}