using AutoMapper;
using Core.Domains.Languages;
using Core.HomePage.HomePageItems;
using Data.Dtos.ChooseUsDTOs;
using Data.Dtos.LanguageDTOs;
using Data.Dtos.NavBarDTOs;
using Data.Dtos.QuestionDTOs;

namespace Data.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Language, LanguageDTO>();
            CreateMap<NavBarItem, NavBarItemDTO>().ReverseMap();
            CreateMap<NavBarItemSubItem, NavBarItemSubItemDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<ChooseUs, ChooseUsDTO>().ReverseMap();
        }
    }
}
