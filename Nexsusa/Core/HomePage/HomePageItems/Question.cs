using Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.HomePageItems
{
    public class Question : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }

        [ForeignKey(nameof(ChooseUsId))]
        public ChooseUs ChooseUs { get; set; }
        public int ChooseUsId { get; set; }

    }
}
