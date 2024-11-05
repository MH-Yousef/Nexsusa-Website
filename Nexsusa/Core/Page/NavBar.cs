using Core.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Page
{
    public class NavBarItem : _Base<int>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool HasSubItem { get; set; }
        public bool IsVisible { get; set; }
        public List<NavBarItemSubItem> NavBarItemSubItems { get; set; }
    }
    public class NavBarItemSubItem : _Base<int>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool IsVisible { get; set; }

        [ForeignKey(nameof(NavBarItemId))]
        public int NavBarItemId { get; set; }
    }
}
