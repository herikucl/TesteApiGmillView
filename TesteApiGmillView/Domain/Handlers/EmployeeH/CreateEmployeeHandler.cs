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
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeRequest, GenericResponse>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public CreateEmployeeHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return GenericResponse.Fail("Objeto invalido!");

            if(request.CompanyId <= 0)
                return GenericResponse.Fail("CompanyId deve ser informado!");

            string[] strings = { request.Name, request.Document, request.Phone };
            string emptyFields = string.Join(", ", Array.FindAll(strings, s => string.IsNullOrWhiteSpace(s)));

            if (!string.IsNullOrEmpty(emptyFields))
                return GenericResponse.Fail($"Os seguintes campos não podem ser nulos: {emptyFields}");

            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == request.CompanyId);

            if(company==null)
                return GenericResponse.Fail("Empresa não encontrada, tente novamente!");


            var employee = _mapper.Map<Employee>(request) ;

            _context.Add(employee);
            await _context.SaveChangesAsync();

            return GenericResponse.Ok("Funcionario adicionado com sucesso!");
        }
    }
}
