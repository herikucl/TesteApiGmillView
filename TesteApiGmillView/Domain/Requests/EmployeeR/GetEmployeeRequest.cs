using MediatR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Requests.EmployeeR
{
    public class GetEmployeeRequest : IRequest<GenericResponse>
    {
        public int Id { get; set; }

    }
}
