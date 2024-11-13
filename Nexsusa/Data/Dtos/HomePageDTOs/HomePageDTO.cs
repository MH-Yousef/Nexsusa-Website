using Core.HomePage.HomePageItems;
using Data.Dtos.BaseDTOs;
using Data.Dtos.ChooseUsDTOs;
using Data.Dtos.ClientSaysDTOs;
using Data.Dtos.FooterDTOs;
using Data.Dtos.NavBarDTOs;
using Data.Dtos.OurCompanyDTOs;
using Data.Dtos.OurEmployeeDTOs;
using Data.Dtos.RegularBlogsDTOs;
using Data.Dtos.ServiceDTOs;
using Data.Dtos.SliderDTOs;
using Data.Dtos.WhoWeAreDTOs;
using Data.Dtos.WorkingProcessDTOs;
using Data.Dtos.WorkShowCaseDTOs;

namespace Data.Dtos.HomePageDTOs
{
    public class HomePageDTO : BaseDTO<int>
    {
        public HomePageInfoDTO HomePageInfo { get; set; }
        public List<NavBarItemDTO> NavBarItems { get; set; }
        public SliderDTO Slider { get; set; }
        public ServiceDTO? Services { get; set; }
        public OurCompanyDTO? OurCompany { get; set; }
        public ChooseUsDTO? ChooseUs { get; set; }
        public WorkingProcessDTO? WorkingProcess { get; set; }
        public WorkShowCaseDTO? WorkShowCase { get; set; }
        public WhoWeAreDTO? WhoWeAre { get; set; }
        public OurEmployeesDTO? OurEmployees { get; set; }
        public ClientSaysDTO? ClientSays { get; set; }
        public RegularBlogsDTO? RegularBlogs { get; set; }
        public FooterDTO? Footer { get; set; }

    }

    public class HomePageInfoDTO : BaseDTO<int>
    {
        public string Title { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaAuthor { get; set; }
        public string MetaPublisher { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<SocialLink> SocialLinks { get; set; }
    }
}
