using MediatR;
using System.Reflection.Metadata.Ecma335;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Requests.ProjectR
{
    public class GetCompanyProjectsRequest : IRequest<List<Project>>
    {
        public int CompanyId { get; set; }
    }
}
