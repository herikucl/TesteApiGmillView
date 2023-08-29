using System.Numerics;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace TesteApiGmillView.Models
{
    public class ProjectView
    {
        public int Id { get;  set; }
        public int CompanyId { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
    }
}
