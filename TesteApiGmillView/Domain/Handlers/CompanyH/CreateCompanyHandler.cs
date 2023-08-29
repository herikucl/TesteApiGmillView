using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.VisualBasic;
using TesteApiGmillView.Context;
using TesteApiGmillView.Domain.Requests.CompanyR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Handlers.CompanyH
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyRequest, GenericResponse>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public CreateCompanyHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return GenericResponse.Fail("Objeto invalido!");

            string[] strings = { request.Name, request.Address };
            string emptyFields = string.Join(", ", Array.FindAll(strings, s => string.IsNullOrWhiteSpace(s)));

            if (!string.IsNullOrEmpty(emptyFields))
                return GenericResponse.Fail($"Os seguintes campos não podem ser nulos: {emptyFields}");

            var company = _mapper.Map<Company>(request);

            _context.Add(company);
            await _context.SaveChangesAsync();

            return GenericResponse.Ok("Empresa adicionada com sucesso!");
        }
    }
}
