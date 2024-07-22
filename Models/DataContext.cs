using Microsoft.EntityFrameworkCore;

namespace App.Models
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<Product> Product { get; set; }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(12,2)");
            });
        }
    }
}