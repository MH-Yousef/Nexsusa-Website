using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.RegularBlogsDTOs
{
    public class RegularBlogsDTO : BaseDTO<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<RegularBlogsItemDTO> RegularBlogsItems { get; set; }
    }

}
