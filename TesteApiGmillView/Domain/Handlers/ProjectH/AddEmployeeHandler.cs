using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TesteApiGmillView.Context;
using TesteApiGmillView.Domain.Requests.EmployeeR;
using TesteApiGmillView.Domain.Requests.ProjectR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Handlers.ProjectH
{
    public class AddEmployeeHandler : IRequestHandler<AddEmployeeToProjectRequest, GenericResponse>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public AddEmployeeHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse> Handle(AddEmployeeToProjectRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return GenericResponse.Fail("Objeto invalido!");

            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == request.EmployeeId);

            if (employee == null)
                return GenericResponse.Ok("Funcionario não encontrado!");

            _context.Add(_mapper.Map<EmployeeProject>(request));
            await _context.SaveChangesAsync();
            return GenericResponse.Ok("O funcionario foi adicionado com sucesso ao projeto!");
        }
    }
}
