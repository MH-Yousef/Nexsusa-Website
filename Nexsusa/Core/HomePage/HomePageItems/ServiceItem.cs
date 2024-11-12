using Core.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.HomePage.HomePageItems
{
    public class ServiceItem : _Base<int>
    {
        [Translatable]
        public string Title { get; set; }
        [Translatable]
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public List<Image> Images { get; set; }


        #region Relations
        [ForeignKey(nameof(ServicesId))]
        public int ServicesId { get; set; }
        #endregion
    }
}
