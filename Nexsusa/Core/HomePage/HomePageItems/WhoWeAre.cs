﻿using Core.Domains;

namespace Core.HomePage.HomePageItems
{
    public class WhoWeAre : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public List<WhoWeAreItem> WhoWeAreItems { get; set; }
    }
}
