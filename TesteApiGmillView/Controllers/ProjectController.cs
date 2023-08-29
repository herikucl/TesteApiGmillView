using MediatR;
using Microsoft.AspNetCore.Mvc;
using TesteApiGmillView.Domain.Commands;
using TesteApiGmillView.Domain.Requests.CompanyR;
using TesteApiGmillView.Domain.Requests.EmployeeR;
using TesteApiGmillView.Domain.Requests.ProjectR;
using TesteApiGmillView.Domain.Response;

namespace TesteApiGmillView.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<GenericResponse> CreateProject([FromBody] CreateProjectRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        [Route("/api/[controller]/employee")]
        public async Task<GenericResponse> AddEmployeeToProject([FromBody] AddEmployeeToProjectRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        public async Task<GenericResponse> EditProject([FromBody] EditProjectRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        public async Task<GenericResponse> GetProject(int id)
        {

            return await _mediator.Send(new GetProjectRequest { Id = id });
        }

        [HttpGet]
        [Route("/api/[controller]/all")]
        public async Task<ActionResult<List<GenericResponse>>> GetProjects()
        {
            try
            {
                var query = new GetProjectsRequest();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<GenericResponse> DeleteProject(int id)
        {
            return await _mediator.Send(new DeleteProjectRequest { Id = id });
        }

    }
}
