using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class AppDbContext : IdentityDbContext<Cliente>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppNetCoreMiercoles;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Categoria> tblCategorias { get; set; }

        public DbSet<Producto> tblProductos { get; set; }

        public DbSet<ItemCarro> tblItemsCarro { get; set; }

        public DbSet<Pedido> tblPedidos { get; set; }

        public DbSet<DetallePedido> tblDetallePedido { get; set; }
    }
}
