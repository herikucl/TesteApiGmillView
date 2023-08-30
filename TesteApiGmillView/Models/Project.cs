using System.Numerics;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace TesteApiGmillView.Models
{
    public class Project
    {
        public Project(int id, int companyId, string name, string description)
        {
            Id = id;
            CompanyId = companyId;
            Name = name;
            Description = description;
        }

        public Project()
        {
                
        }

		public Project(int id, string name, string description, string status, int companyId)
		{
			Id = id;
			Name = name;
			Description = description;
			Status = status;
			CompanyId = companyId;
		}

        public Project(Project project)
        {
            Id = project.Id;
            CompanyId = project.CompanyId;
            Name = project.Name;
            Description = project.Description;
            Status = project.Status;
        }


        public int Id { get; private set; }
        public int CompanyId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public string Status { get; private set; }

        public ICollection<EmployeeProject> EmployeeProject { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public Company Company { get; set; }


    }
}
