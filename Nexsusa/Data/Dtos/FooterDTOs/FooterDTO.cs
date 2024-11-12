using Core.Domains;
using Data.Dtos.BaseDTOs;
using Data.Dtos.FooterServiceDTOs;
using Data.Dtos.QuickLinkDTOs;

namespace Data.Dtos.FooterDTOs
{
    public class FooterDTO:BaseDTO<int>
    {
        [Translatable]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<FooterServiceDTO> Services { get; set; }
        public List<QuickLinkDTO> QuickLinks { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
