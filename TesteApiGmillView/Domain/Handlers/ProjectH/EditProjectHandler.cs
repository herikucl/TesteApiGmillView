using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TesteApiGmillView.Context;
using TesteApiGmillView.Domain.Requests.CompanyR;
using TesteApiGmillView.Domain.Requests.EmployeeR;
using TesteApiGmillView.Domain.Requests.ProjectR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Handlers.ProjectH
{
    public class EditProjectHandler : IRequestHandler<EditProjectRequest, GenericResponse>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public EditProjectHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse> Handle(EditProjectRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return GenericResponse.Fail("Objeto invalido!");

            string[] strings = {request.Description};
            string emptyFields = string.Join(", ", Array.FindAll(strings, s => string.IsNullOrWhiteSpace(s)));

            if (!string.IsNullOrEmpty(emptyFields))
                return GenericResponse.Fail($"Os seguintes campos não podem ser nulos: {emptyFields}");

            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (project == null)
                return GenericResponse.Fail("Projeto não encontrado, tente novamente!");

            project = _mapper.Map<EditProjectRequest, Project>(request, project);

            _context.Update(project);
            await _context.SaveChangesAsync();

            return GenericResponse.Ok("Projeto modificado com sucesso!");
        }
    }
}
