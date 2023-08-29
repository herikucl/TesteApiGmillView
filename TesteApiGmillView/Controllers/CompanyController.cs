using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TesteApiGmillView.Domain.Commands;
using TesteApiGmillView.Domain.Requests.CompanyR;
using TesteApiGmillView.Domain.Requests.EmployeeR;
using TesteApiGmillView.Domain.Response;

namespace TesteApiGmillView.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<GenericResponse> CreateCompany([FromBody] CreateCompanyRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        public async Task<GenericResponse> EditCompany([FromBody] EditCompanyRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        public async Task<GenericResponse> GetCompany(int id)
        {
            return await _mediator.Send(new GetCompanyRequest { Id = id });
        }

        [HttpGet]
        [Route("/api/[controller]/all")]
        public async Task<ActionResult<List<GenericResponse>>> GetCompanies()
        {
            try
            {
                var query = new GetCompaniesRequest();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/[controller]/all/Employees")]
        public async Task<ActionResult<List<GenericResponse>>> GetCompanyEmployees(int companyId)
        {
            try
            {
                var query = new GetCompanyEmployeesRequest { CompanyId = companyId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/[controller]/all/Projects")]
        public async Task<ActionResult<List<GenericResponse>>> GetCompanyProjects(int companyId)
        {
            try
            {
                var query = new Domain.Requests.ProjectR.GetCompanyProjectsRequest { CompanyId = companyId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<GenericResponse> DeleteCompany(int id)
        { 
            return await _mediator.Send(new DeleteCompanyRequest { Id = id });
        }
    }
}
