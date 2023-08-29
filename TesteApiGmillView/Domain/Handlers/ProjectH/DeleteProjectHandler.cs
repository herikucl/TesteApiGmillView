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
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectRequest, GenericResponse>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public DeleteProjectHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return GenericResponse.Fail("Objeto invalido!");

            var project = await _context.Employees.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (project == null)
                return GenericResponse.Fail("Projeto não encontrado, tente novamente!");

            _context.Remove(project);
            await _context.SaveChangesAsync();

            return GenericResponse.Ok("Projeto removido com sucesso!");
        }
    }
}
