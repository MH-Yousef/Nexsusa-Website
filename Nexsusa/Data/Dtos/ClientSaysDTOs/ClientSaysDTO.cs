using Core.Domains;
using Data.Dtos.BaseDTOs;
using Data.Dtos.ClientSaysItemDTOs;

namespace Data.Dtos.ClientSaysDTOs
{
    public class ClientSaysDTO:BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public List<ClientSaysItemDTO> ClientSaysItems { get; set; }
    }
}
