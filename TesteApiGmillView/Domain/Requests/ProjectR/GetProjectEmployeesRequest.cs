using MediatR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Requests.ProjectR
{
    public class GetProjectEmployeesRequest : IRequest<List<Employee>>
    {
        public int Id {get;set;}
    }
}
