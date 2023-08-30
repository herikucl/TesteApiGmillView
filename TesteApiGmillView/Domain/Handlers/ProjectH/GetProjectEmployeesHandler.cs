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
    public class GetEmployeeProjectsHandler : IRequestHandler<GetProjectEmployeesRequest, List<Employee>>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public GetEmployeeProjectsHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Employee>> Handle(GetProjectEmployeesRequest request, CancellationToken cancellationToken)
        {

			var result = await _context.EmployeesProjects.Where(x => x.ProjectId == request.Id).ToListAsync();

            var employees = new List<Employee>();
            foreach (var item in result)
            {
                var search = await _context.Employees.FirstOrDefaultAsync(x => x.Id == item.EmployeeId);
                employees.Add(new Employee(search.Id,search.Name,search.Document,search.Phone, search.CompanyId));
			}
            
			if (employees == null)
				throw new Exception("Nenhum projeto foi encontrado nesta empresa");
			return employees;
		}
    }
}
