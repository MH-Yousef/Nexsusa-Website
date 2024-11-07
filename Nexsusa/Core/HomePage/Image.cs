using Core.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.HomePage
{
    public class Image : _Base<int>
    {
        [ForeignKey(nameof(ServiceItemId))]
        public int ServiceItemId { get; set; }
        public string ImageUrl { get; set; }
    }
}
