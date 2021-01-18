using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using dummy.db.entity;

namespace dummy.db.context
{
    public partial class lachlanbarclaynet2Context : DbContext
    {
        public lachlanbarclaynet2Context()
        {
        }

        public lachlanbarclaynet2Context(DbContextOptions<lachlanbarclaynet2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostComment> PostComment { get; set; }
        public virtual DbSet<PostType> PostType { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=localhost;initial catalog=lachlanbarclaynet2;user id=lachlanbarclaynetuser;password=lachlanbarclaynetpassword;MultipleActiveResultSets=True;App=LbNet");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Published).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.PostType)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.PostTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__PostTypeID__4222D4EF");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.Property(e => e.PostCommentId).ValueGeneratedNever();

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostComment)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_PostComment_Post");
            });

            modelBuilder.Entity<PostType>(entity =>
            {
                entity.Property(e => e.PostTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CCAC70ABE55B");

                entity.Property(e => e.QrCodeTemp).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
