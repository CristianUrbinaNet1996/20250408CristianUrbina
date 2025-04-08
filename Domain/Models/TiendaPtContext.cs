using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public partial class TiendaPtContext : DbContext
{
    public TiendaPtContext()
    {
    }

    public TiendaPtContext(DbContextOptions<TiendaPtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DiscountProduct> DiscountProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<sp_GetAllProducts> Sp_GetAllProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DiscountProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Discount__3214EC07D9AD2D95");

            entity.ToTable("DiscountProduct");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DiscountPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("discountPrice");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.DiscountProducts)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_DiscountProd_PRod");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC077E5B9A80");

            entity.ToTable("Product");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
