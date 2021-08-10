using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Models
{
    public class Site
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string DisplayName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Status { get; set; }
        public int? AccountOrganizationId { get; set; }
    }
}
