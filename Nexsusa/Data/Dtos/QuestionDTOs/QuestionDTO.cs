using Core.Domains;
using Data.Dtos.BaseDTOs;

namespace Data.Dtos.QuestionDTOs
{
    public class QuestionDTO : BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public int ChooseUsId { get; set; }
    }

}
