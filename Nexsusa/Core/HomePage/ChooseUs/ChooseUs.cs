using Core.Domains;
using Core.HomePage.ChooseUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.ChooseUs
{
    public class ChooseUs : _Base<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<Question> Questions { get; set; }
    }
}
