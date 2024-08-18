using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Filtro
    {
        public string? NombreProducto { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public float? PrecioMin { get; set; }
        public float? PrecioMax { get; set; }
    }
}
