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

        public Producto producto { get; set; }

        public int Cantidad { get; set; }

        public int IdCarrito { get; set;}

        public Carrito carrito { get; set; }

        public Compra()
        {
            Console.WriteLine();
        }
    }
}
