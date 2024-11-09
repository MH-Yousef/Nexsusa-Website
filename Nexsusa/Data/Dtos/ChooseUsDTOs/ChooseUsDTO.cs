using Core.HomePage.HomePageItems;
using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.ChooseUsDTOs
{
    public class ChooseUsDTO:BaseDTO<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<Question> Questions { get; set; }
    }
}
