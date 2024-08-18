using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entidades
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Tipo { get; set; }
        public string Modelo { get; set;}
        public int Existencias { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Foto> Fotos { get; set; }
        public float Precio { get; set; }

        public int IdCompra {  get; set; }

        [JsonIgnore]
        public Compra Compra { get; set; }


    }
}
