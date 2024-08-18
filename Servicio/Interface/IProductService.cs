using Entidades;

namespace Servicio.Interface
{
    public interface IProductService
    {
        Task<Producto> CreateProduct(Producto newProduct);
        Task<bool> DeleteProduct(int id);
        Task<bool> EditProduct(int id, Producto updatedProduct);
        Task<IEnumerable<Producto>> GetAllProducts();
        Task<IEnumerable<Producto>> GetFilteredProducts(Filtro objFiltro);
    }
}