using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Entidades
{
    public class AppDbContext : DbContext
    {
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Reseña> Reseñas { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Carrito>()
                .Property(ca => ca.IdCarrito)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Compra>()
                .Property(co  => co.IdCompra)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Producto>()
                .Property(p => p.IdProducto)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reseña>()
                .Property(r => r.IdReseña)
                .ValueGeneratedOnAdd();


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=catalogo.db");
            }
        }
    }
}
