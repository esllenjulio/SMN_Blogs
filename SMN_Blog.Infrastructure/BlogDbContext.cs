

using Microsoft.EntityFrameworkCore;
using SMN_Blog.Domain.Entities;

namespace SMN_Blog.Infrastructure
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>();

            modelBuilder.Entity<Comment>()
                           .HasOne(c => c.Post)
                           .WithMany(p => p.Comments)
                           .HasForeignKey(c => c.PostId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
