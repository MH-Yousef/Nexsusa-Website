using Core.Domains;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.WorkShowCaseDTOs
{
    public class WorkShowCaseDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public List<WorkShowCaseItemDTO> WorkShowCaseItems { get; set; }
    }

}
