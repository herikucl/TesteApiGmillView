using MediatR;
using TesteApiGmillView.Domain.Response;

namespace TesteApiGmillView.Domain.Requests.ProjectR
{
    public class CreateProjectRequest : IRequest<GenericResponse>
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
