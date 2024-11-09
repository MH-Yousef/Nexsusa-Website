using Core.HomePage.HomePageItems;
using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.ClientSaysItemDTOs
{
    public class ClientSaysItemDTO:BaseDTO<int>
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Branch { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey(nameof(ClientSaysId))]
        public int ClientSaysId { get; set; }
       
    }
}
