using System;
using System.Linq;
using PfBlog.Models;

namespace PfBlog.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BlogContext context)
        {

            context.Database.EnsureCreated();

            if (context.Blogs.Any())
            { return; }

            var blogs = new Blog[]
        {  new Blog {Author ="Alice",Time=DateTime.Parse("2017-12-11"),Title="identity保护api",Category="框架和第三方库"},
           new Blog {Author ="Bob",Time=DateTime.Parse("2018-02-05"),Title="Azure Fuctions",Category="云服务"},
           new Blog {Author ="Jsen",Time=DateTime.Parse("1928-10-25"),Title="连接数据库",Category="框架和第三方库"},
           new Blog {Author ="Jack",Time=DateTime.Parse("2016-11-23"),Title="Timejob定时包",Category="框架和第三方库"},
        };
            foreach (Blog s in blogs)
            {
                context.Blogs.Add(s);
            }
            context.SaveChanges();

          

            var tags = new Tag[]
            {
            new Tag{Text="ASP.NET core"},
            new Tag{Text="EF框架"},
            new Tag{Text="Identity Server4"},
            new Tag{Text="Time Job"},
            };
            foreach (Tag c in tags)
            {
                context.Tags.Add(c);
            }
            context.SaveChanges();


            var blogtags = new BlogTag[]
            {
              new BlogTag{BlogId = 1,TagId = 1},
              new BlogTag{BlogId = 2,TagId = 2},
              new BlogTag{BlogId = 3,TagId = 2},
              new BlogTag{BlogId = 3,TagId = 3},

            };
            foreach (BlogTag v in blogtags)
            {
                context.BlogTags.Add(v);
            }
            context.SaveChanges();
            

        }
    }
}
