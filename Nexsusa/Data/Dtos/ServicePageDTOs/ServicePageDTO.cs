using Core.Domains;
using Data.Dtos.BaseDTOs;
using Data.Dtos.ServiceDTOs;

namespace Data.Dtos.ServicePageDTOs
{
    public class ServicePageDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        public ServiceDTO Service { get; set; }
    }
}
