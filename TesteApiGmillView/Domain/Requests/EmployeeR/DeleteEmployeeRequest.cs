using MediatR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Requests.EmployeeR
{
    public class DeleteEmployeeRequest : IRequest<GenericResponse>
    {
        public int Id { get; set; }

    }
}
