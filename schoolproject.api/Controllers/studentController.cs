using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using schoolproject.api.bases;
using schoolproject.core;
using schoolproject.core.feature.student.command.model;
using schoolproject.core.feature.student.query.model;
using schoolproject.data.appmetadata;
using schoolproject.data.entites;
using schoolproject.api.bases;

namespace schoolproject.api.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    
    public class studentController : appcontrollerbases
    {
       /* private readonly IMediator _mediator;

        public studentController(IMediator mediator)
        {
            _mediator = mediator;
        }*/
       
        //[HttpGET("/Student/List")]
        [HttpGet(router.studentrouting.getstudentlist)]
        public async Task<IActionResult> GetStudentsListAsync()
        {
            var query = new getstudentlistquery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(router.studentrouting.getstudentbyarr)]
        public async Task<IActionResult> GetspecificparamaStudentsAsync()
        {
            var query = new getspecificattributefromstdudent();
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        //[HttpGet("Student/{id})]
        [HttpGet(router.studentrouting.getstudentbyid )]
        public async Task<IActionResult> GetStudentsbyid([FromRoute] int id)
        {
        
            var result = await Mediator.Send(new getstudentbyidquery(id));//{Id=id}); mmkn a3ml kda lw na msh 3ayz a3ml constructor f l model
           return NewResult(result);
            // return Ok(result);
        }
        [HttpPost(router.studentrouting.addstd )]
        public async Task<IActionResult> addstudent([FromBody] addstudentcommand student)
        {
        
            var result = await Mediator.Send(student);
            return NewResult(result); 
            //  return Ok(result);
        }
        [HttpPost(router.studentrouting.editstd )]
        public async Task<IActionResult> editstudent([FromBody] editstudent student)
        {
        
            var result = await Mediator.Send(student);
            return NewResult(result); 
            //  return Ok(result);
        }




    }
}
