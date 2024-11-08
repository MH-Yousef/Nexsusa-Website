using Core.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.HomePage.Services
{
    public class ServiceItem : _Base<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }


        #region Relations
        [ForeignKey(nameof(ServicesId))]
        public int ServicesId { get; set; }
        #endregion
    }
}
