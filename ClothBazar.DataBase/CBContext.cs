using ClothBazar.Entity;
using System;
using System.Data.Entity;

namespace ClothBazar.DataBase
{
    public class CBContext : DbContext, IDisposable
    {
        public CBContext() : base("ClothBazar")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Config> Configurations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
