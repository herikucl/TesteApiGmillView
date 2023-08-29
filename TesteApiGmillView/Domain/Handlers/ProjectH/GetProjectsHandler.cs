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

namespace TesteApiGmillView.Domain.Handlers.ProjectsH
{
    public class GetProjectsHandler : IRequestHandler<GetProjectsRequest, List<Project>>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public GetProjectsHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Project>> Handle(GetProjectsRequest request, CancellationToken cancellationToken)
        {

            var projects = await _context.Projects.ToListAsync();

            if (projects == null)
                throw new Exception("Nenhum projeto foi encontrado");

            return projects;
        }
    }
}
