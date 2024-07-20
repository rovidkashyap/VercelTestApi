using Microsoft.EntityFrameworkCore;

namespace VercelTestApi.Models.DataContext
{
    public class VercelDbContext : DbContext
    {
        public VercelDbContext(DbContextOptions<VercelDbContext> options)
            :base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Category Entity
            modelBuilder.Entity<Category>(entity =>
            {
                // Configure CategoryName Property
                entity.Property(e => e.CategoryName)
                        .IsRequired()
                        .HasMaxLength(50);

                // Configure Primary Key
                entity.HasKey(e => e.CategoryId);
            });

            //Configure Product Entity
            modelBuilder.Entity<Product>(entity =>
            {
                // Configure ProductName property
                entity.Property(e => e.ProductName)
                        .IsRequired()
                        .HasMaxLength(250);

                // Configure Product Price property
                entity.Property(e => e.ProductPrice)
                        .IsRequired()
                        .HasColumnType("decimal(5, 2)");
            });

            // Configure Relationship between Product and Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
