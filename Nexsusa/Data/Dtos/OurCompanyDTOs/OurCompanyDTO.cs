using Core.Domains;
using Data.Dtos.BaseDTOs;
using Microsoft.AspNetCore.Http;

namespace Data.Dtos.OurCompanyDTOs
{
    public class OurCompanyDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile File { get; set; }
    }

}
