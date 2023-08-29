using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TesteApiGmillView.Context;
using TesteApiGmillView.Domain.Requests.CompanyR;
using TesteApiGmillView.Domain.Response;

namespace TesteApiGmillView.Domain.Handlers.CompanyH
{
    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyRequest, GenericResponse>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public DeleteCompanyHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse> Handle(DeleteCompanyRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return GenericResponse.Fail("Objeto invalido!");

            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (company == null)
                return GenericResponse.Fail("Empresa não encontrada, tente novamente!");

            _context.Remove(company);
            await _context.SaveChangesAsync();

            return GenericResponse.Ok("Empresa removida com sucesso!");
        }
    }
}
