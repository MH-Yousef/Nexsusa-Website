using Core.Domains;

namespace Core.HomePage.HomePageItems
{
    public class Service : _Base<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ServiceItem> ServiceItem { get; set; }
    }
}
