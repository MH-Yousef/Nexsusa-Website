using Core.Domains;

namespace Core.HomePage.HomePageItems
{
    public class Slider : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
    }
}
