using Core.Domains;

namespace Core.HomePage.HomePageItems
{
    public class OurEmployees : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public List<OurEmployeesItem> OurEmployeesItems { get; set; }
    }
}
