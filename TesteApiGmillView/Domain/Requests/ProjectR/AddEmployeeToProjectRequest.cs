using MediatR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Requests.ProjectR
{
    public class AddEmployeeToProjectRequest : IRequest<GenericResponse>
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
    }
}
