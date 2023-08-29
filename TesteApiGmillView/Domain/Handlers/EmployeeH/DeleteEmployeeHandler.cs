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
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeRequest, GenericResponse>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public DeleteEmployeeHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse> Handle(DeleteEmployeeRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return GenericResponse.Fail("Objeto invalido!");

            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (employee == null)
                return GenericResponse.Fail("Funcionario não encontrada, tente novamente!");

            _context.Remove(employee);
            await _context.SaveChangesAsync();

            return GenericResponse.Ok("Funcionario removido com sucesso!");
        }
    }
}
