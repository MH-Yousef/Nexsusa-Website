using Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.HomePageItems
{
    public class WhoWeAreItem : _Base<int>
    {
        public string Description { get; set; }
        [ForeignKey(nameof(WhoWeAreId))]
        public int WhoWeAreId { get; set; }
        public WhoWeAre WhoWeAre { get; set; }
    }
}
