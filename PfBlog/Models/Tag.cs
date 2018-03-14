
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PfBlog.Models
{
    public class Tag
    {   
        public int TagId { get; set; }

        [MaxLength(10, ErrorMessage = "标签不能超过20个字段！")]
        public string Text { get; set; }

        public ICollection<BlogTag> BlogTags { get; set; }
    }
}
 