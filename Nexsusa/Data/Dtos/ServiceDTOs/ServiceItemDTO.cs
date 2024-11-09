using Data.Dtos.BaseDTOs;
using Data.Dtos.ImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.ServiceDTOs
{
    public class ServiceItemDTO : BaseDTO<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public List<ImageDTO> Images { get; set; }
    }

}
