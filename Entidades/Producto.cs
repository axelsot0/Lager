﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Marca { get; set; }
        public string Tipo { get; set; }
        public string Modelo { get; set;}
        public int Existencias { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public float Precio { get; set; }
        public int IdReseña { get; set; }
        public Reseña Reseña { get; set;
        


    }
}
