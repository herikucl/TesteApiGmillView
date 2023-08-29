using MediatR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Requests.EmployeeR
{
    public class GetEmployeesRequest : IRequest<List<Employee>>
    {
    }
}
