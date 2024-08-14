using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Compra
    {
        public int IdCompra { get; set; }

        public int IdProducto { get; set; }

        public ICollection<Producto> Productos { get; set; }

        public int Cantidad { get; set; }

        public int IdUsuario { get; set;}

        public ApplicationUser usuario { get; set; }

        public DateTime Fecha { get; set; }

        
    }
}
