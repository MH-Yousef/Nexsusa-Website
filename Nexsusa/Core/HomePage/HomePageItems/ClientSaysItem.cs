using Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.HomePageItems
{
    public class ClientSaysItem : _Base<int>
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Branch { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey(nameof(ClientSaysId))]
        public int ClientSaysId { get; set; }
        public ClientSays ClientSays { get; set; }
    }
}
