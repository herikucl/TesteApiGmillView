using MediatR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Requests.ProjectR
{
    public class GetProjectsRequest : IRequest<List<Project>>
    {
    }
}
