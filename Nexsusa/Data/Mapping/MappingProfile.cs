using AutoMapper;
using Core.Domains.Languages;
using Core.HomePage.HomePageItems;
using Data.Dtos.LanguageDTOs;
using Data.Dtos.NavBarDTOs;

namespace Data.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Language, LanguageDTO>();
            CreateMap<NavBarItem, NavBarItemDTO>().ReverseMap();
            //CreateMap<NavBarItemSubItem, NavBarItemSubItemDTO>();
        }
    }
}
