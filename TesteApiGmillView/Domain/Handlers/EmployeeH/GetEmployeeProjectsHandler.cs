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

namespace TesteApiGmillView.Domain.Handlers.EmployeeH
{
    public class GetEmployeeProjectsHandler : IRequestHandler<GetEmployeeProjectsRequest, List<Project>>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public GetEmployeeProjectsHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Project>> Handle(GetEmployeeProjectsRequest request, CancellationToken cancellationToken)
        {

			var result = await _context.EmployeesProjects.Where(x => x.EmployeeId == request.Id).ToListAsync();

            var projects = new List<Project>();
            foreach (var item in result)
            {
                var search = await _context.Projects.FirstOrDefaultAsync(x => x.Id == item.ProjectId);
				projects.Add(new Project(search.Id,search.Name,search.Description,search.Status, search.CompanyId));
			}
            
			if (projects == null)
				throw new Exception("Nenhum projeto foi encontrado nesta empresa");
			return projects;
		}
    }
}
