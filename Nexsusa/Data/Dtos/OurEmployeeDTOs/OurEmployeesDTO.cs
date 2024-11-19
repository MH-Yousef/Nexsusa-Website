using Core.Domains;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.OurEmployeeDTOs
{
    public class OurEmployeesDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public List<OurEmployeesItemDTO> OurEmployeesItems { get; set; }
    }
}
