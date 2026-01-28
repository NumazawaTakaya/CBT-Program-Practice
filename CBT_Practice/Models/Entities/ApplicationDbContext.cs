using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CBT_Practice.Models.Entities;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ADAPTIVE_THOUGHT_EMOTION> ADAPTIVE_THOUGHT_EMOTIONs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=cbt_app;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ADAPTIVE_THOUGHT_EMOTION>(entity =>
        {
            entity.ToTable("ADAPTIVE_THOUGHT_EMOTIONS");

            entity.Property(e => e.CREATED_AT)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.EMOTION).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
