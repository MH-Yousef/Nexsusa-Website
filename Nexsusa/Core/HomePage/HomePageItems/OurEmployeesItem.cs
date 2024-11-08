using Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HomePage.HomePageItems
{
    public class OurEmployeesItem : _Base<int>
    {
        public string FullName { get; set; }
        public string Branch { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey(nameof(OurEmployeesId))]
        public int OurEmployeesId { get; set; }
        public OurEmployees OurEmployees { get; set; }
    }
}
