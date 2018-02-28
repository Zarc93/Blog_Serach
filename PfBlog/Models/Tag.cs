
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PfBlog.Models
{
    public class Tag
    {   
        public int TagId { get; set; }

        public string Text { get; set; }

        public ICollection<BlogTag> BlogTags { get; set; }
    }
}
 