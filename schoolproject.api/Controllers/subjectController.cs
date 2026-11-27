using MediatR;
using Microsoft.AspNetCore.Mvc;
using schoolproject.api.bases;
using schoolproject.core.feature.subject.command.model;
using schoolproject.core.feature.subject.query.model;
using schoolproject.data.appmetadata;

namespace schoolproject.api.Controllers
{
    [ApiController]
    public class subjectController : appcontrollerbases
    {
        [HttpGet(router.subjectrouting.getsubjectlist)]
        public async Task<IActionResult> GetSubjectsListAsync()
        {
            var result = await Mediator.Send(new getsubjectlistquery());
            return Ok(result);
        }

        [HttpGet(router.subjectrouting.getsubjectbyid)]
        public async Task<IActionResult> GetSubjectbyid([FromRoute] int id)
        {
            var result = await Mediator.Send(new getsubjectbyidquery(id));
            return NewResult(result);
        }

        [HttpGet(router.subjectrouting.getsubjectpaginited)]
        public async Task<IActionResult> getsubjectpaginite([FromQuery] getsubjectpaginatedlistquery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(router.subjectrouting.addsubject)]
        public async Task<IActionResult> addsubject([FromBody] addsubjectcommand subject)
        {
            var result = await Mediator.Send(subject);
            return NewResult(result);
        }

        [HttpPut(router.subjectrouting.editsubject)]
        public async Task<IActionResult> editsubject([FromBody] editsubject subject)
        {
            var result = await Mediator.Send(subject);
            return NewResult(result);
        }

        [HttpDelete(router.subjectrouting.deletesubject)]
        public async Task<IActionResult> deletesubject([FromRoute] int id)
        {
            var result = await Mediator.Send(new deletesubjectcommand(id));
            return NewResult(result);
        }
    }
}
