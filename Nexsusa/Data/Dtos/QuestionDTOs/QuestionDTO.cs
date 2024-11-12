using Core.Domains;
using Data.Dtos.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.QuestionDTOs
{
    public class QuestionDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
    }

}
