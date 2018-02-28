using PfBlog.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace PfBlog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext()
        {
        }

        public BlogContext (DbContextOptions<BlogContext> options) : base(options)
         {
         }

         public DbSet<Blog> Blogs { get; set; }

         public DbSet<BlogTag> BlogTags { get; set; }

         public DbSet<Tag> Tags { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<Blog>().ToTable("Blog");
                

             modelBuilder.Entity<BlogTag>().ToTable("BlogTag")
                 .HasOne(a => a.Tag).WithMany(a => a.BlogTags)
                 .HasForeignKey(a => a.TagId);
             modelBuilder.Entity<BlogTag>().HasOne(a => a.Blog).WithMany(a => a.BlogTags);

            modelBuilder.Entity<Tag>().ToTable("Tag");
            
        }

        internal object Include(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}