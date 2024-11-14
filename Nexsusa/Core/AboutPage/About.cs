using Core.Domains;
using Core.HomePage.HomePageItems;

namespace Core.AboutPage
{
    public class About : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string SubTitle { get; set; }
        [Translatable]
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public OurCompany OurCompany { get; set; }
        public WhoWeAre WhoWeAre { get; set; }
        public ClientSays ClientSays { get; set; }
    }
}
