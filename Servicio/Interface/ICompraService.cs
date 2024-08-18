using Entidades;

namespace Servicio.Interface
{
    public interface ICompraService
    {
        Task<Compra> CreateCompra(Compra newCompra);
        Task<bool> DeleteCompra(int id);
        Task<IEnumerable<Compra>> GetAllCompras();
        Task<IEnumerable<Compra>> GetComprasByUser(int idUser);
    }
}