using CBT_Practice.Models;
using CBT_Practice.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CBT_Practice.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AUTO_THOUGHT> AUTO_THOUGHTs { get; set; }

        public virtual DbSet<EMOTION> EMOTIONs { get; set; }

        public virtual DbSet<EVIDENCE> EVIDENCEs { get; set; }

        public virtual DbSet<SEVEN_COLUMN> SEVEN_COLUMNs { get; set; }

        public virtual DbSet<SITUATION> SITUATIONs { get; set; }

        public virtual DbSet<dbTest> dbTests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=cbt_app;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AUTO_THOUGHT>(entity =>
            {
                entity.ToTable("AUTO_THOUGHTS");

                entity.Property(e => e.AUTO_THOUGHT1)
                    .HasMaxLength(500)
                    .HasColumnName("AUTO_THOUGHT");
                entity.Property(e => e.CREATED_AT)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.SEVEN_COLUMNS).WithMany(p => p.AUTO_THOUGHTs)
                    .HasForeignKey(d => d.SEVEN_COLUMNS_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AUTO_THOUGHTS_SEVEN_COLUMNS");
            });

            modelBuilder.Entity<EMOTION>(entity =>
            {
                entity.ToTable("EMOTIONS");

                entity.Property(e => e.CREATED_AT)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(sysdatetime())");
                entity.Property(e => e.EMOTION1)
                    .HasMaxLength(50)
                    .HasColumnName("EMOTION");

                entity.HasOne(d => d.THOUGHTS).WithMany(p => p.EMOTIONs)
                    .HasForeignKey(d => d.THOUGHTS_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMOTIONS_AUTO_THOUGHTS");
            });

            modelBuilder.Entity<EVIDENCE>(entity =>
            {
                entity.ToTable("EVIDENCES");

                entity.Property(e => e.CORE_BELIEF).HasMaxLength(1000);
                entity.Property(e => e.COUNTER_EVIDENCE).HasMaxLength(1000);
                entity.Property(e => e.CREATED_AT)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(sysdatetime())");
                entity.Property(e => e.EVIDENCE1)
                    .HasMaxLength(1000)
                    .HasColumnName("EVIDENCE");
                entity.Property(e => e.INSIDE_BELIEF).HasMaxLength(1000);

                entity.HasOne(d => d.THOUGHTS).WithMany(p => p.EVIDENCEs)
                    .HasForeignKey(d => d.THOUGHTS_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVIDENCES_AUTO_THOUGHTS");
            });

            modelBuilder.Entity<SEVEN_COLUMN>(entity =>
            {
                entity.ToTable("SEVEN_COLUMNS");

                entity.Property(e => e.TITLE).HasMaxLength(200);
            });

            modelBuilder.Entity<SITUATION>(entity =>
            {
                entity.ToTable("SITUATIONS");

                entity.Property(e => e.APPROACH).HasMaxLength(200);
                entity.Property(e => e.CHARACTER_FROM).HasMaxLength(50);
                entity.Property(e => e.CHARACTER_TO).HasMaxLength(50);
                entity.Property(e => e.CREATED_AT)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(sysdatetime())");
                entity.Property(e => e.HAPPEND_PLACE).HasMaxLength(100);
                entity.Property(e => e.HAPPEND_TIME).HasPrecision(0);
                entity.Property(e => e.HAPPEND_TIME_DETAIL).HasMaxLength(100);
                entity.Property(e => e.OTHER_INFO).HasMaxLength(500);
                entity.Property(e => e.PROPOSAL_OBJECT).HasMaxLength(200);
                entity.Property(e => e.UPDATED_AT)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.SEVEN_COLUMNS).WithMany(p => p.SITUATIONs)
                    .HasForeignKey(d => d.SEVEN_COLUMNS_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SITUATIONS_SEVEN_COLUMNS");
            });

            modelBuilder.Entity<dbTest>(entity =>
            {
                entity.Property(e => e.ChangeType).HasDefaultValue("");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
