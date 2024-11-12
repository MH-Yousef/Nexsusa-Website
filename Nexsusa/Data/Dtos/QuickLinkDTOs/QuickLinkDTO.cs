using Core.Domains;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.QuickLinkDTOs
{
    public class QuickLinkDTO:BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
