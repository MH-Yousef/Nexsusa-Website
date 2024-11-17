using AutoMapper;
using Core.AboutPage;
using Core.Domains.Languages;
using Core.HomePage;
using Core.HomePage.HomePageItems;
using Core.ServicesPage;
using Data.Dtos.AboutDTOs;
using Data.Dtos.ChooseUsDTOs;
using Data.Dtos.ClientSaysDTOs;
using Data.Dtos.ClientSaysItemDTOs;
using Data.Dtos.FooterDTOs;
using Data.Dtos.FooterServiceDTOs;
using Data.Dtos.HomePageDTOs;
using Data.Dtos.LanguageDTOs;
using Data.Dtos.NavBarDTOs;
using Data.Dtos.OurCompanyDTOs;
using Data.Dtos.OurEmployeeDTOs;
using Data.Dtos.QuestionDTOs;
using Data.Dtos.QuickLinkDTOs;
using Data.Dtos.RegularBlogsDTOs;
using Data.Dtos.ServiceDTOs;
using Data.Dtos.ServicePageDTOs;
using Data.Dtos.SliderDTOs;
using Data.Dtos.WhoWeAreDTOs;
using Data.Dtos.WorkingProcessDTOs;
using Data.Dtos.WorkShowCaseDTOs;

namespace Data.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Language, LanguageDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<HomePage, HomePageDTO>().ReverseMap();
            CreateMap<HomePageInfo, HomePageInfoDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<About, AboutDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<ServicePage, ServicePageDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<NavBarItem, NavBarItemDTO>().ReverseMap();
            CreateMap<NavBarItemSubItem, NavBarItemSubItemDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<ChooseUs, ChooseUsDTO>().ReverseMap().ForMember(x => x.Questions, opt => opt.MapFrom(src => src.Questions));
            CreateMap<Question, QuestionDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<ClientSays, ClientSaysDTO>().ReverseMap().ForMember(x => x.ClientSaysItems, opt => opt.MapFrom(src => src.ClientSaysItems));
            CreateMap<ClientSaysItem, ClientSaysItemDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<Footer, FooterDTO>().ReverseMap();
            CreateMap<FooterService, FooterServiceDTO>().ReverseMap();
            CreateMap<QuickLink, QuickLinkDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<OurCompany, OurCompanyDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<OurEmployees, OurEmployeesDTO>().ReverseMap();
            CreateMap<OurEmployeesItem, OurEmployeesItemDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<RegularBlogs, RegularBlogsDTO>().ReverseMap();
            CreateMap<RegularBlogsItem, RegularBlogsItemDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<Service, ServiceDTO>().ReverseMap().ForMember(x => x.ServiceItems, opt => opt.MapFrom(src => src.ServiceItems));
            CreateMap<ServiceItem, ServiceItemDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<Slider, SliderDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<WhoWeAre, WhoWeAreDTO>().ReverseMap();
            CreateMap<WhoWeAreItem, WhoWeAreItemDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<WorkingProcess, WorkingProcessDTO>().ReverseMap().ForMember(x => x.WorkingProcessItems, opt => opt.MapFrom(src => src.WorkingProcessItems));
            CreateMap<WorkingProcessItem, WorkingProcessItemDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<WorkShowCase, WorkShowCaseDTO>().ReverseMap();
            CreateMap<WorkShowCaseNavBar, WorkShowCaseNavBarDTO>().ReverseMap();
            CreateMap<WorkShowCaseNavBarItem, WorkShowCaseNavBarItemDTO>().ReverseMap();
            CreateMap<WorkShowCaseService, WorkShowCaseServiceDTO>().ReverseMap();
            //====================================================================================================
        }
    }
}
