using Core.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.HomePage.HomePageItems
{
    public class Footer : _Base<int>
    {
        [Translatable]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<FooterService> Services { get; set; }
        public List<QuickLink> QuickLinks { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
    public class QuickLink : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        public string Url { get; set; }
        public int FooterId { get; set; }
        [ForeignKey(nameof(FooterId))]
        public Footer Footer { get; set; }
    }
    public class FooterService : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        public string Url { get; set; }
        public int FooterId { get; set; }
        [ForeignKey(nameof(FooterId))]
        public Footer Footer { get; set; }
    }
}
