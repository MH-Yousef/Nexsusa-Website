using Core.Domains;
using Data.Dtos.BaseDTOs;
using Microsoft.AspNetCore.Http;

namespace Data.Dtos.WorkingProcessDTOs
{
    public class WorkingProcessItemDTO : BaseDTO<int>
    {
        public string ImageUrl { get; set; }
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public int WorkingProcessId { get; set; }

        public IFormFile File { get; set; }
    }
}
