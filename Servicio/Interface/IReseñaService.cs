using Entidades.Entity;

namespace Servicio.Interface
{
    public interface IReseñaService
    {
        Task<Reseña> CreateReseña(Reseña newReseña);
        Task<bool> DeleteReseña(int idReseña);
        Task<IEnumerable<Reseña>> GetAllReseñas();
        Task<IEnumerable<Reseña>> GetReseñasByProducts(int idProducto);
    }
}