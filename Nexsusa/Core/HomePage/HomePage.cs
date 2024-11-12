using Core.Domains;
using Core.HomePage.HomePageItems;

namespace Core.HomePage
{
    public class HomePage : _Base<int>
    {
        public HomePageInfo HomePageInfo { get; set; }
        public List<NavBarItem> NavBarItems { get; set; }
        public Slider Slider { get; set; }
        public Service Services { get; set; }
        public OurCompany OurCompany { get; set; }
        public ChooseUs ChooseUs { get; set; }
        public WorkingProcess WorkingProcess { get; set; }
        public WorkShowCase WorkShowCase { get; set; }
        public WhoWeAre WhoWeAre { get; set; }
        public OurEmployees OurEmployees { get; set; }
        public ClientSays ClientSays { get; set; }
        public RegularBlogs RegularBlogs { get; set; }

    }
    public class HomePageInfo : _Base<int>
    {
        public string Title { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaAuthor { get; set; }
        public string MetaPublisher { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<SocialLink> SocialLinks { get; set; }
    }
}
