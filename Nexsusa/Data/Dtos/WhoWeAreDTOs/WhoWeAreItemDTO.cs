using Core.Domains;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.WhoWeAreDTOs
{
    public class WhoWeAreItemDTO : BaseDTO<int>
    {
        [Translatable]
        public string Description { get; set; }
        public int WhoWeAreId { get; set; }
    }

}
