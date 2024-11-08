using Core.Domains;

namespace Core.HomePage.HomePageItems
{
    public class OurCompany : _Base<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
