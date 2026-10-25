using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using schoolproject.core;
using schoolproject.core.feature.student.query.model;
using schoolproject.data.appmetadata;

namespace schoolproject.api.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class studentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public studentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(router.studentrouting.getstudentlist)]
        public async Task<IActionResult> GetStudentsListAsync()
        {
            var query = new getstudentlistquery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(router.studentrouting.getstudentbyarr)]
        public async Task<IActionResult> GetspecificparamaStudentsAsync()
        {
            var query = new getspecificattributefromstdudent();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(router.studentrouting.getstudentbyid )]
        public async Task<IActionResult> GetStudentsbyid([FromRoute] int id)
        {
        
            var result = await _mediator.Send(new getstudentbyidquery(id));//{Id=id}); mmkn a3ml kda lw na msh 3ayz a3ml constructor f l model
            return Ok(result);
        }




    }
}
