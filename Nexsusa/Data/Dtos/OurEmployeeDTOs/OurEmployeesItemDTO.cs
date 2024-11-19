using Data.Dtos.BaseDTOs;
using Microsoft.AspNetCore.Http;

namespace Data.Dtos.OurEmployeeDTOs
{
    public class OurEmployeesItemDTO : BaseDTO<int>
    {
        public string FullName { get; set; }
        public string Branch { get; set; }
        public string ImageUrl { get; set; }
        public int OurEmployeesId { get; set; }
        public IFormFile File { get; set; }
    }
}
