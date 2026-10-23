using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using schoolproject.core;
using schoolproject.core.feature.student.query.model;

namespace schoolproject.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class studentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public studentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("/student/list")]
        public async Task<IActionResult> GetStudentsListAsync()
        {
            var query = new getstudentlistquery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


    }
}
