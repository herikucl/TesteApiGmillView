using MediatR;
using TesteApiGmillView.Domain.Response;

namespace TesteApiGmillView.Domain.Requests.ProjectR
{
    public class GetProjectRequest : IRequest<GenericResponse>
    {
        public int Id { get; set; }
    }
}
