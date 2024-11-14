using Core.Domains;
using Data.Dtos.BaseDTOs;
using Data.Dtos.ClientSaysDTOs;
using Data.Dtos.OurCompanyDTOs;
using Data.Dtos.WhoWeAreDTOs;

namespace Data.Dtos.AboutDTOs
{
    public class AboutDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string SubTitle { get; set; }
        [Translatable]
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public OurCompanyDTO OurCompany { get; set; }
        public WhoWeAreDTO WhoWeAre { get; set; }
        public ClientSaysDTO ClientSays { get; set; }
    }
}
