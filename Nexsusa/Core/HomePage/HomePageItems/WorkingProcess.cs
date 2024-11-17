using Core.Domains;

namespace Core.HomePage.HomePageItems
{
    public class WorkingProcess : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string SubTitle { get; set; }
        public List<WorkingProcessItem> WorkingProcessItems { get; set; }
    }
}
