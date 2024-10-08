﻿using System.Text.Json.Serialization;

namespace Entidades.Entity
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Tipo { get; set; }
        public string Modelo { get; set; }
        public int Existencias { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Foto> Fotos { get; set; }
        public string IdUser { get; set; }
        public float Precio { get; set; }







        [JsonIgnore]
        public int? IdCompra { get; set; }

        [JsonIgnore]
        public Compra Compra { get; set; }
    }
}
