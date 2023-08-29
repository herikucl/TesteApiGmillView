using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TesteApiGmillView.Context;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Domain.Commands
{
    public class GetCompaniesHandler : IRequestHandler<GetCompaniesRequest, List<Company>>
    {
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;
        public GetCompaniesHandler(CompanyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Company>> Handle(GetCompaniesRequest request, CancellationToken cancellationToken)
        {

            var companies = await _context.Companies.ToListAsync();

            if (companies == null)
                throw new Exception("Nenhuma empresa foi encontrada");

            return companies;
        }
    }
}
