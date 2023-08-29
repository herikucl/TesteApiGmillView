using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TesteApiGmillView.Context;
using TesteApiGmillView.Domain.Requests.EmployeeR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Handlers.EmployeeH
{
    public class GetEmployeeHandler : IRequestHandler<GetEmployeeRequest, GenericResponse>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public GetEmployeeHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse> Handle(GetEmployeeRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return GenericResponse.Fail("Objeto invalido!");

            var employeee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (employeee == null)
                return GenericResponse.Fail("Funcionario não encontrado, tente novamente!");

            return GenericResponse.Ok<Employee>(employeee);
        }
    }
}
