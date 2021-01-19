namespace LachlanBarclayNet.DAO
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LbNet : DbContext
    {
        public LbNet()
            : base("name=LbNet")
        {
        }

        public virtual DbSet<PostOld> Posts { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }
        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<UserOld> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostType>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.PostType)
                .HasForeignKey(e => e.PostTypeID)
                .WillCascadeOnDelete(false);
        }
    }
}
