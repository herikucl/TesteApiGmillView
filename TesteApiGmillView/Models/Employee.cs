using System.ComponentModel.Design;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TesteApiGmillView.Models
{
    public class Employee
    {
        public Employee( string name, string document, string phone)
        {
            Name = name;
            Document = document;
            Phone = phone;
        }

        public Employee(int id, string name, string document, string phone, int companyId)
        {
            Id = id;
            Name = name;
            Document = document;
            Phone = phone;
            CompanyId = companyId;
        }

        public Employee(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            Document = employee.Document;
            Phone = employee.Phone;
            CompanyId = employee.CompanyId;
        }

		public Employee(int id, string name, string document, string phone)
		{
			Id = id;
			Name = name;
			Document = document;
			Phone = phone;
		}

		public int Id { get; private set; }
        public int CompanyId { get; private set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Phone { get; private set; }

        public ICollection<EmployeeProject> EmployeeProject { get; set; }
        public ICollection<Project> Projects { get; set; }
        public Company Company { get; set; }
    }
}
