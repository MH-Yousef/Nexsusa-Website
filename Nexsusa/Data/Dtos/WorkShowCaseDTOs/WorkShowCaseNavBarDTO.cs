using Core.Domains;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.WorkShowCaseDTOs
{
    public class WorkShowCaseNavBarDTO : BaseDTO<int>
    {
        [Translatable]
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool HasSubItem { get; set; }
        public bool IsVisible { get; set; }
        public List<WorkShowCaseItemDTO> WorkShowCaseNavBarItems { get; set; }
        public int WorkShowCaseId { get; set; }
    }

}
