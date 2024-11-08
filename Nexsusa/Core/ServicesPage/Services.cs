using Core.Domains;
using Core.HomePage.HomePageItems;

namespace Core.ServicesPage
{
    public class Services : _Base<int>
    {
        public Service Service { get; set; }
    }
}
