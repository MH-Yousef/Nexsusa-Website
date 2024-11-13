using Core.Domains;

namespace Core.HomePage.HomePageItems
{
    public class ContactUs:_Base<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
