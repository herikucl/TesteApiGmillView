using MediatR;
using TesteApiGmillView.Domain.Response;

namespace TesteApiGmillView.Domain.Requests.ProjectR
{
    public class EditProjectRequest : IRequest<GenericResponse>
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
