using Core.Domains;

namespace Core.HomePage.HomePageItems
{
    public class WorkShowCase : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public List<WorkShowCaseItem> WorkShowCaseItems { get; set; }
    }
}
