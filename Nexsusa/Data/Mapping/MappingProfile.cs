using AutoMapper;
using Core.Domains.Languages;
using Data.Dtos.LanguageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Language, LanguageDTO>();
          
        
        }
    }
}
