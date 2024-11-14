using Core.Domains;

namespace Core.HomePage.HomePageItems
{
    public class SocialLink : _Base<int>
    {
        public string Url { get; set; }
        public string Name { get; set; }
    }
}
