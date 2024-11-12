using Core.Domains;
using Core.HomePage.HomePageItems;
using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.RegularBlogsDTOs
{

    public class RegularBlogsItemDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Content { get; set; }
        public int RegularBlogsId { get; set; }
    }


}
