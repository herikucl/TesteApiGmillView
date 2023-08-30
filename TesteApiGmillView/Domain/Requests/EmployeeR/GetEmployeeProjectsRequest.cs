using MediatR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Requests.EmployeeR
{
    public class GetEmployeeProjectsRequest : IRequest<List<Project>>
    {
        public int Id { get; set; }
    }
}
