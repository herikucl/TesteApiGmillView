using MediatR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Requests.CompanyR
{
    public class DeleteCompanyRequest : IRequest<GenericResponse>
    {
        public int Id { get; set; }
    }
}
