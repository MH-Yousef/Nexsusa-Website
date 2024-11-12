using Core.Domains;
using Data.Dtos.BaseDTOs;
using Data.Dtos.QuestionDTOs;

namespace Data.Dtos.ChooseUsDTOs
{
    public class ChooseUsDTO:BaseDTO<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<QuestionDTO> Questions { get; set; }
    }
}
