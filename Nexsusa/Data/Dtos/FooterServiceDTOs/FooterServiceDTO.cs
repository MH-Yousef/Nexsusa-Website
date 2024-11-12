using Core.Domains;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.FooterServiceDTOs
{
    public class FooterServiceDTO:BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
