using Core.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.HomePage.HomePageItems
{
    public class WorkShowCaseItem : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string SubTitle { get; set; }
        [Translatable]
        public string Description { get; set; }
        [Translatable]
        public string SubDescription { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }


        [ForeignKey(nameof(WorkShowCaseId))]
        public WorkShowCase WorkShowCase { get; set; }
        public int WorkShowCaseId { get; set; }
    }
}
