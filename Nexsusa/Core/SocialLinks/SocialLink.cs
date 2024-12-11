using Core.Domains;

namespace Core.SocialLinks
{
    public class SocialLink : _Base<int>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconClass { get; set; }
    }
}
