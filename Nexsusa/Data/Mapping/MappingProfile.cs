using AutoMapper;
using Core.Domains.Languages;
using Core.HomePage;
using Core.HomePage.HomePageItems;
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
            CreateMap<Language, LanguageDTO>();
            //====================================================================================================
            CreateMap<HomePage, HomePageDTO>();
            CreateMap<HomePageInfo, HomePageInfoDTO>();
            //====================================================================================================
            CreateMap<NavBarItem, NavBarItemDTO>().ReverseMap();
            CreateMap<NavBarItemSubItem, NavBarItemSubItemDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<ChooseUs, ChooseUsDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<ClientSays, ClientSaysDTO>().ReverseMap();
            CreateMap<ClientSaysItem, ClientSaysItemDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<Footer, FooterDTO>().ReverseMap();
            CreateMap<FooterService, FooterServiceDTO>().ReverseMap();
            CreateMap<QuickLink, QuickLinkDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<OurCompany, OurCompanyDTO>().ReverseMap();
            //====================================================================================================
            CreateMap<OurEmployees, OurEmployeesDTO>();
            CreateMap<OurEmployeesItem, OurEmployeesItemDTO>();
            //====================================================================================================
            CreateMap<RegularBlogs, RegularBlogsDTO>();
            CreateMap<RegularBlogsItem, RegularBlogsItemDTO>();
            //====================================================================================================
            CreateMap<Service, ServiceDTO>();
            CreateMap<ServiceItem, ServiceItemDTO>();
            //====================================================================================================
            CreateMap<Slider, SliderDTO>();
            //====================================================================================================
            CreateMap<WhoWeAre, WhoWeAreDTO>();
            CreateMap<WhoWeAreItem, WhoWeAreItemDTO>();
            //====================================================================================================
            CreateMap<WorkingProcess, WorkingProcessDTO>();
            CreateMap<WorkingProcessItem, WorkingProcessItemDTO>();
            //====================================================================================================
            CreateMap<WorkShowCase, WorkShowCaseDTO>();
            CreateMap<WorkShowCaseNavBar, WorkShowCaseNavBarDTO>();
            CreateMap<WorkShowCaseNavBarItem, WorkShowCaseNavBarItemDTO>();
            CreateMap<WorkShowCaseService, WorkShowCaseServiceDTO>();
            //====================================================================================================
        }
    }
}
