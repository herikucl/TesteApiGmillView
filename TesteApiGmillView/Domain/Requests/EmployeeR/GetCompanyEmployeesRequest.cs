using MediatR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Requests.EmployeeR
{
    public class GetCompanyEmployeesRequest : IRequest<List<Employee>>
    {
        public int CompanyId { get; set; }
    }
}
