using Core.Domains;

namespace Core.AboutPage
{
    public class About : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string SubTitle { get; set; }
        [Translatable]
        public string Description { get; set; }
        public string VideoUrl { get; set; }
    }
}
