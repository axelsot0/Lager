using Entidades.Entity;
using Microsoft.AspNetCore.Http;

namespace Entidades.Dtos.Entity
{
    public class ProductDTO
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Tipo { get; set; }
        public string Modelo { get; set; }
        public int Existencias { get; set; }
        public string Descripcion { get; set; }
        public List<IFormFile> Fotos { get; set; }
        public string IdUser { get; set; }
        public float Precio { get; set; }
    }
}
