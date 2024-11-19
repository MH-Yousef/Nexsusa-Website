using Core.Domains;
using Data.Dtos.BaseDTOs;
using Microsoft.AspNetCore.Http;

namespace Data.Dtos.ServiceDTOs
{
    public class ServiceItemDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int ServiceId { get; set; }
        public string Images1 { get; set; }
        public string Images2 { get; set; }
        public string Images3 { get; set; }
        public IFormFile File { get; set; }
        public IFormFile Image1File { get; set; }
        public IFormFile Image2File { get; set; }
        public IFormFile Image3File { get; set; }
    }

}
