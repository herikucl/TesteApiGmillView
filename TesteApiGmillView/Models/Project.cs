using System.Numerics;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace TesteApiGmillView.Models
{
    public class Project
    {
        public int Id { get; private set; }
        public int CompanyId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime ReleaseDate { get; private set; } = DateTime.UtcNow;
        public DateTime DeliveryDate { get; private set; }

        public string Status { get; private set; }

        public ICollection<EmployeeProject> EmployeeProject { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public Company Company { get; set; }


    }
}
