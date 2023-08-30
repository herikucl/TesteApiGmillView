using AutoMapper;
using AutoMapper.Internal;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TesteApiGmillView.Context;
using TesteApiGmillView.Domain.Commands;
using TesteApiGmillView.Domain.Requests.EmployeeR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Handlers.CompanyH
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
            var result = await _context.Companies.FirstOrDefaultAsync(x => x.Id == request.CompanyId);

            if (result == null)
                throw new Exception("Empresa não encontrada");

            List<Employee> employees = new List<Employee>();
            var data = await _context.Employees.Where(x => x.CompanyId == request.CompanyId).ToListAsync();
            foreach (var item in data)
            {
                employees.Add(new Employee(item));
            }
            if (employees.Count == 0)
                throw new Exception("Nenhum funcionario foi encontrado nesta empresa");
            return employees;
        }
    }
}
