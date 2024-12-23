﻿using Core.Domains;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.NavBarDTOs
{
    public class NavBarItemDTO : BaseDTO<int>
    {
        [Translatable]
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool HasSubItem { get; set; }
        public bool IsVisible { get; set; }
        public List<NavBarItemSubItemDTO> NavBarItemSubItems { get; set; } 
    }

}
