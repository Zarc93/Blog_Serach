using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft;
using System.ComponentModel.DataAnnotations;

namespace PfBlog.Models
{
    public class BlogTag
    {
        public int BlogTagId { get; set; }

        public int TagId { get; set; }
        public int BlogId { get; set; }

        public Blog Blog { get; set; }
        public Tag Tag { get; set; }
    }
}
