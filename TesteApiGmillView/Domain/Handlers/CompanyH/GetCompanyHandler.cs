using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TesteApiGmillView.Context;
using TesteApiGmillView.Domain.Requests.CompanyR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Handlers.CompanyH
{
    public class GetCompanyHandler : IRequestHandler<GetCompanyRequest, GenericResponse>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public GetCompanyHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse> Handle(GetCompanyRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return GenericResponse.Fail("Objeto invalido!");

            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (company == null)
                return GenericResponse.Fail("Empresa não encontrada, tente novamente!");

            return GenericResponse.Ok<Company>(company);
        }
    }
}
