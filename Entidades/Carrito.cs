using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entidades
{
    public class Carrito
    {
        public int IdCarrito { get; set; }

        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        public DateTime FechaCreacion { get; set; }

        [JsonIgnore]
        public ICollection<Compra> compras { get; set; }
    }
}
