using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PfBlog.Models;
using PfBlog.Data;

namespace PfBlog.Controllers
{

    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private readonly BlogContext _context;

        public BlogController(BlogContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult Search_Posts(string index)
        {
            ///对输入关键字格式进行 筛选

            //无关键字
            var blog = Search_Blog(index);
            var tag = Search_Tag(index);
            if (blog == null && tag == null)
            {
                return new EmptyResult();
            }

            //按照Blog->Tag ,Tag->Blog进行两次搜索
            var posts = GetWithBlog(index);
            var posts2 = GetWithTag(index);

            //合并这两个查询去重 并升序
            var result = new List<Posts>();
            result = posts.Union(posts2).OrderBy(p => p.BlogId).ToList();

            return Ok(result);
        }


        #region 利用中间表检索

        /// Blog->Tag 记录到posts
        public List<Posts> GetWithBlog(string index)
        {
            var blog = Search_Blog(index);
            var tag = Search_Tag(index);

            int count = 0;
            var posts = new List<Posts>
            {

            };

            foreach (Blog b in blog)
            {
                posts.Add(new Posts(0, "empty", "empty", "empty", DateTime.Today, new List<string> { "empty" }));
                posts[count].BlogId = b.BlogId;
                posts[count].Author = b.Author;
                posts[count].Category = b.Category;
                posts[count].Time = b.Time;
                posts[count++].Title = b.Title;
            }

            bool clear = true;

            for (int i = 0; i < count; i++)
            {
                foreach (BlogTag s in _context.BlogTags)
                {
                    if (s.BlogId == posts[i].BlogId)
                    {

                        foreach (Tag t in _context.Tags)
                        {
                            if (t.TagId == s.TagId)
                            {
                                if (clear) { posts[i].Tag.Clear(); clear = false; }
                                posts[i].Tag.Add(t.Text);
                            }
                        }
                    }

                }
                clear = true;
            }
            return posts;
        }

        /// Tag->Blog记录到posts
        public List<Posts> GetWithTag(string index)
        {
            var tags = Search_Tag(index);
            var posts = new List<Posts>
            {

            };
            var list = new List<int>();//记录对应的Blogid - 以免重复生成Blog 
            var list2 = new List<int>();//记录对应的Blogid - 直到Blog 查询完所有的Tag 才生成新的Blog对象
            int count = -1;
            bool spik = false;
            bool clear = true;

            foreach (Tag t in tags)
            {
                foreach (BlogTag s in _context.BlogTags)
                {
                    if (s.TagId == t.TagId)
                    {
                        //查看 --Blog -> Tag中如果已经包含有此Tag->Blog的对象，跳过 
                        var blog = Search_Blog(index);
                        foreach (Blog a in blog)
                        {
                            if (a.BlogId == s.BlogId)
                            {
                                spik = true;
                                break;
                            }

                        }
                        if (spik) { break; }

                        foreach (BlogTag g in _context.BlogTags)
                        {
                            if (g.BlogId == s.BlogId)
                            {

                                if (list.Contains(s.BlogId)) { break; }

                                var Findtag = _context.Tags.Where(p => p.TagId == g.TagId).ToList();

                                if (list2.Contains(s.BlogId))
                                {
                                    posts[count].Tag.Add(Findtag[0].Text);
                                }
                                else
                                {
                                    count++;
                                    list2.Add(s.BlogId);
                                    posts.Add(new Posts(0, "empty", "empty", "empty", DateTime.Today, new List<string> { "empty" }));
                                    var Findblog = _context.Blogs.Where(b => b.BlogId == s.BlogId).ToList();
                                    posts[count].BlogId = s.BlogId;
                                    posts[count].Author = Findblog[0].Author;
                                    posts[count].Category = Findblog[0].Category;
                                    posts[count].Title = Findblog[0].Title;
                                    posts[count].Tag.Clear();
                                    posts[count].Tag.Add(Findtag[0].Text);
                                    posts[count].Time = Findblog[0].Time;
                                }

                            }

                        }
                        list.Add(s.BlogId);
                        //  else //多个Tag对应一个Blog的情况
                        //   {
                        //    int Findblog = posts.FindIndex(c => c.BlogId == s.BlogId);
                        //    posts[Findblog].Tag.Add(t.Text);
                        //  }
                    }
                    spik = false;
                }


            }
            return posts;
        }
        #endregion

        #region 关键字查询
        /// Blog 查询方法     
        public IEnumerable<Blog> Search_Blog(string goal)
        {
            return (from p in _context.Blogs
                    where p.Author.Contains(goal)
                    || p.Category.Contains(goal)
                    || p.Title.Contains(goal)
                    || p.Time.ToString("yyyy-MM-dd").Contains(goal)
                    orderby p.BlogId
                    select p)
                     .AsNoTracking()
                     .ToList();
        }
        /// Tag 查询方法
        public IEnumerable<Tag> Search_Tag(string goal)
        {
            return (from p in _context.Tags
                    where p.Text.Contains(goal)
                    select p)
                     .AsNoTracking()
                     .ToList();
        }

        #endregion

    }
}
