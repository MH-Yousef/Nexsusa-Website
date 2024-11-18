using Core.Domains;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.WorkShowCaseDTOs
{
    public class WorkShowCaseItemDTO : BaseDTO<int>
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

        public int WorkShowCaseId { get; set; }
    }

}
