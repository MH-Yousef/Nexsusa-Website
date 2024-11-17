using Core.Domains;

namespace Core.HomePage.HomePageItems
{
    public class Service : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public List<ServiceItem> ServiceItems { get; set; }
    }
}
