using Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.HomePageItems
{
    public class RegularBlogsItem : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        [ForeignKey(nameof(RegularBlogsId))]
        public int RegularBlogsId { get; set; }
        public RegularBlogs RegularBlogs { get; set; }
    }
}
