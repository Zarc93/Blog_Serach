using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PfBlog.Models
{   
    public class Posts
    {
        public int? BlogId { get; set; }

        
        [MaxLength(20, ErrorMessage = "标题不能超过20个字段！")]
        public  string Title { get; set; }

 
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public  DateTime Time  { get; set; } 

        public  string Author { get; set; }

        [MaxLength(20, ErrorMessage = "范畴不能超过20个字段！")]
        public string Category { get; set; } 

        public List<string> Tag { get; set; }

        public Posts(int blogid ,string author  ,string category  ,string title  ,DateTime time ,List<string> tag )
        {
            BlogId = blogid;
            Author = author;
            Category = category;
            Title = title;
            Time = time;           
            Tag = tag;
        }

        

}
}
