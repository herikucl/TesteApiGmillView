using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TesteApiGmillView.Context;
using TesteApiGmillView.Domain.Requests.CompanyR;
using TesteApiGmillView.Domain.Requests.EmployeeR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Handlers.EmployeeH
{
    public class EditEmployeeHandler : IRequestHandler<EditEmployeeRequest, GenericResponse>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public EditEmployeeHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse> Handle(EditEmployeeRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return GenericResponse.Fail("Objeto invalido!");

            string[] strings = { request.Name, request.Document, request.Phone };
            string emptyFields = string.Join(", ", Array.FindAll(strings, s => string.IsNullOrWhiteSpace(s)));

            if (!string.IsNullOrEmpty(emptyFields))
                return GenericResponse.Fail($"Os seguintes campos não podem ser nulos: {emptyFields}");

            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (employee == null)
                return GenericResponse.Fail("Funcionario não encontrado, tente novamente!");

            employee = _mapper.Map<EditEmployeeRequest, Employee>(request, employee);

            _context.Update(employee);
            await _context.SaveChangesAsync();

            return GenericResponse.Ok("Funcionario modificado com sucesso!");
        }
    }
}
