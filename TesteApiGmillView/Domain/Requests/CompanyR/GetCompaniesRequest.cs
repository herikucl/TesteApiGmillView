using MediatR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Commands
{
    public class GetCompaniesRequest : IRequest<List<Company>>
    {
    }
}
