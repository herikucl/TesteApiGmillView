using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TesteApiGmillView.Context;
using TesteApiGmillView.Domain.Commands;
using TesteApiGmillView.Domain.Requests.EmployeeR;
using TesteApiGmillView.Domain.Requests.ProjectR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Handlers.ProjectH
{
    public class GetCompanyProjectsHandler : IRequestHandler<GetCompanyProjectsRequest, List<Project>>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public GetCompanyProjectsHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Project>> Handle(GetCompanyProjectsRequest request, CancellationToken cancellationToken)
        {
        var projects = await _context.Projects.Where(x => x.CompanyId == request.CompanyId).ToListAsync();

            if (projects == null)
                throw new Exception("Nenhum projeto foi encontrado nesta empresa");
            return projects;
        }
    }
}
