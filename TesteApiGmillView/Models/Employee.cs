using System.ComponentModel.Design;
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
