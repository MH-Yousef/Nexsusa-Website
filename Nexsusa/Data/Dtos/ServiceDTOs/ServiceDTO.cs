using Core.Domains;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.ServiceDTOs
{
    public class ServiceDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public List<ServiceItemDTO> ServiceItems { get; set; }
    }

}
