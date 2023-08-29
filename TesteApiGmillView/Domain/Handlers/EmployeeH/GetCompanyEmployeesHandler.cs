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
    public class GetCompanyEmployeesHandler : IRequestHandler<GetCompanyEmployeesRequest, List<Employee>>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public GetCompanyEmployeesHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Employee>> Handle(GetCompanyEmployeesRequest request, CancellationToken cancellationToken)
        {
        var employees = await _context.Employees.Where(x => x.CompanyId == request.CompanyId).ToListAsync();

            if (employees == null)
                throw new Exception("Nenhum funcionario foi encontrado nesta empresa");
            return employees;
        }
    }
}
