using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Reseña
    {
        public int Id { get; set; }

        public int IdProducto { get; set; }

        public Producto Producto { get; set; }

        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        public string Comentario { get; set; }

        public DateTime Fecha { get; set; }
    }
}
