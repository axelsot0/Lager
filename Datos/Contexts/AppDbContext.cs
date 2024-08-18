using Microsoft.EntityFrameworkCore;
using Entidades;
using Entidades.Entity;
namespace Datos
{
    public class AppDbContext : DbContext
    {
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Reseña> Reseñas { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            


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

            modelBuilder.Entity<Compra>()
                .HasKey(c => c.IdCompra);

            

            modelBuilder.Entity<Producto>()
                .HasKey(p => p.IdProducto);

            modelBuilder.Entity<Reseña>()
                .HasKey(r => r.IdReseña);


            modelBuilder.Entity<Producto>()
                .HasOne(c => c.Compra)
                .WithMany(p => p.Productos)
                .HasForeignKey(c => c.IdCompra)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Producto_Compra");


            modelBuilder.Entity<Foto>().HasKey(f => f.Id);

            modelBuilder.Entity<Producto>()
                .HasMany(f => f.Fotos)
                .WithOne(p => p.Producto)
                .HasForeignKey(p => p.IdProducto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Producto_Foto");
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
