using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DoAn1_DDG_Pro.Identity;

namespace DoAn1_DDG_Pro.Models;

public partial class ShopDdgContext : DbContext
{

    public ShopDdgContext()
    {
    }

    public ShopDdgContext(DbContextOptions<ShopDdgContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductType> ProductTypes { get; set; } 
    
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DATTOON\\DATER;Initial Catalog=ShopDDG;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED9805162B");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Imgbot)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("imgbot");
            entity.Property(e => e.Imgtop)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("imgtop");
            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Type).WithMany(p => p.Products)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Products__TypeID__398D8EEE");
        });

       

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__ProductT__516F03956099DA1A");

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.TypeName).HasMaxLength(255);
        });

       

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
