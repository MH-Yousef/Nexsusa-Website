﻿namespace Core.Domains.Languages
{
    public class Language: _Base<int>
    {
        public string Name { get; set; }
        //====================================================================================================
        public string Shortcut { get; set; }
        //====================================================================================================
        public string Culture { get; set; }
        //====================================================================================================
        public bool IsRtl { get; set; }
        //====================================================================================================
        public bool IsDefault { get; set; }
        //====================================================================================================
        public bool IsActive { get; set; }
        //====================================================================================================
        public List<StringResource> StringResources { get; set; }
    }
}
