using Core.Domains;
using Data.Dtos.BaseDTOs;
using Data.Dtos.QuickLinkDTOs;
using Data.Dtos.ServiceDTOs;
using Microsoft.AspNetCore.Http;

namespace Data.Dtos.FooterDTOs
{
    public class FooterDTO:BaseDTO<int>
    {
        [Translatable]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile File { get; set; }
        public List<ServiceDTO> Services { get; set; }
        public List<QuickLinkDTO> QuickLinks { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Translatable]
        public string Address { get; set; }
    }
}
