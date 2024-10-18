using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace De02.Model
{
    public partial class SanPhamContexDB : DbContext
    {
        public SanPhamContexDB()
            : base("name=SanPhamContexDB")
        {
        }

        public virtual DbSet<LOAISP> LOAISPs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LOAISP>()
                .Property(e => e.MALOAI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LOAISP>()
                .HasMany(e => e.SANPHAMs)
                .WithRequired(e => e.LOAISP)
                .HasForeignKey(e => e.MALOAI)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LOAISP>()
                .HasMany(e => e.SANPHAMs1)
                .WithRequired(e => e.LOAISP1)
                .HasForeignKey(e => e.MALOAI)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.MASP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.MALOAI)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
