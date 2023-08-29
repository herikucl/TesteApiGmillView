using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TesteApiGmillView.Context;
using TesteApiGmillView.Domain.Commands;
using TesteApiGmillView.Domain.Requests.EmployeeR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Handlers.EmployeeH
{
    public class GetProjectsHandler : IRequestHandler<GetEmployeesRequest, List<Employee>>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public GetProjectsHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Employee>> Handle(GetEmployeesRequest request, CancellationToken cancellationToken)
        {

            var employees = await _context.Employees.ToListAsync();

            if (employees == null)
                throw new Exception("Nenhum funcionario foi encontrado");

            return employees;
        }
    }
}
