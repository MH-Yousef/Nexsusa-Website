using Core.Domains;
using Core.HomePage.HomePageItems;

namespace Core.ServicesPage
{
    public class ServicePage : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        public Service Service { get; set; }
    }
}
