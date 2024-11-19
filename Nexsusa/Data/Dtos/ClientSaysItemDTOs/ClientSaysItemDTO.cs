using Core.Domains;
using Data.Dtos.BaseDTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Dtos.ClientSaysItemDTOs
{
    public class ClientSaysItemDTO:BaseDTO<int>
    {
        public string FullName { get; set; }
        [Translatable]
        public string Description { get; set; }
        public string Branch { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey(nameof(ClientSaysId))]
        public int ClientSaysId { get; set; }
       
    }
}
