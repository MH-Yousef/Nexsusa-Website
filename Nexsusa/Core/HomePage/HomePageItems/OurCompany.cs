using Core.Domains;

namespace Core.HomePage.HomePageItems
{
    public class OurCompany : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
