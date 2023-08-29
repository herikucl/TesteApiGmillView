using MediatR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Requests.EmployeeR
{
    public class CreateEmployeeRequest : IRequest<GenericResponse>
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }

    }
}
