using Core.Domains;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.WorkingProcessDTOs
{
    public class WorkingProcessDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string SubTitle { get; set; }
        public List<WorkingProcessItemDTO> WorkingProcessItems { get; set; }
    }
}
