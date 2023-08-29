using MediatR;
using Microsoft.AspNetCore.Mvc;
using TesteApiGmillView.Domain.Commands;
using TesteApiGmillView.Domain.Requests.CompanyR;
using TesteApiGmillView.Domain.Requests.EmployeeR;
using TesteApiGmillView.Domain.Response;

namespace TesteApiGmillView.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<GenericResponse> CreateEmployee([FromBody] CreateEmployeeRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        public async Task<GenericResponse> EditEmployee([FromBody] EditEmployeeRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        public async Task<GenericResponse> GetEmployee(int id)
        {

            return await _mediator.Send(new GetEmployeeRequest { Id = id });
        }

        [HttpGet]
        [Route("/api/[controller]/all")]
        public async Task<ActionResult<List<GenericResponse>>> GetEmployees()
        {
            try
            {
                var query = new GetEmployeesRequest();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<GenericResponse> DeleteEmployee(int id)
        {
            return await _mediator.Send(new DeleteEmployeeRequest { Id = id });
        }

    }
}
