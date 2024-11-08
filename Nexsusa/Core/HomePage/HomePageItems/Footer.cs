using Core.Domains;

namespace Core.HomePage.HomePageItems
{
    public class Footer : _Base<int>
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<FooterService> services { get; set; }
        public List<QuickLink> QuickLinks { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
    public class QuickLink : _Base<int>
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
    public class FooterService : _Base<int>
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
