using MediatR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Requests.CompanyR
{
    public class CreateCompanyRequest : IRequest<GenericResponse>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
