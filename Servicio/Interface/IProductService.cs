using Entidades;
using Entidades.Dtos.Entity;
using Entidades.Entity;

namespace Servicio.Interface
{
    public interface IProductService
    {
        Task<Producto> CreateProduct(ProductDTO product);
        Task<bool> EditProduct(int id, ProductDTO updatedProduct);
        Task<bool> DeleteProduct(int id);
        Task<IEnumerable<Producto>> GetAllProducts();
        Task<IEnumerable<Producto>> GetFilteredProducts(FiltroProducts objFiltro);
    }
}