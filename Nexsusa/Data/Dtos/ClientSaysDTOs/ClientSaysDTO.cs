using Core.Domains;
using Core.HomePage.HomePageItems;
using Data.Dtos.BaseDTOs;
using Data.Dtos.ClientSaysItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
