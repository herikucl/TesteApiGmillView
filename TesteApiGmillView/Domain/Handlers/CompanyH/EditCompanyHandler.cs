using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TesteApiGmillView.Context;
using TesteApiGmillView.Domain.Requests.CompanyR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Handlers.CompanyH
{
    public class EditCompanyHandler : IRequestHandler<EditCompanyRequest, GenericResponse>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public EditCompanyHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse> Handle(EditCompanyRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return GenericResponse.Fail("Objeto invalido!");

            string[] strings = { request.Name, request.Address };
            string emptyFields = string.Join(", ", Array.FindAll(strings, s => string.IsNullOrWhiteSpace(s)));

            if (!string.IsNullOrEmpty(emptyFields))
                return GenericResponse.Fail($"Os seguintes campos não podem ser nulos: {emptyFields}");

            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (company == null)
                return GenericResponse.Fail("Empresa não encontrada, tente novamente!");

            company = _mapper.Map<EditCompanyRequest, Company>(request, company);

            _context.Update(company);
            await _context.SaveChangesAsync();

            return GenericResponse.Ok("Empresa modificada com sucesso!");
        }
    }
}
