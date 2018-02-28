using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PfBlog.Models
{
    public class Blog
    {
        public int BlogId { get; set; }

        [MaxLength(20, ErrorMessage = "标题不能超过20个字段！")]
        public string Title { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        public string Author { get; set; }

        [MaxLength(20, ErrorMessage = "范畴不能超过20个字段！")]
        public string Category { get; set; }

        public ICollection<BlogTag> BlogTags { get; set; }


    }

}