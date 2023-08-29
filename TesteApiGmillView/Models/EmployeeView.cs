using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TesteApiGmillView.Models
{
    public class EmployeeView
    {

        public int Id { get; set; }
        public virtual int CompanyId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        
    }
}
