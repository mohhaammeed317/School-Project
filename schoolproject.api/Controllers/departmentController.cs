using MediatR;
using Microsoft.AspNetCore.Mvc;
using schoolproject.api.bases;
using schoolproject.core.feature.department.command.model;
using schoolproject.core.feature.department.query.model;
using schoolproject.data.appmetadata;

namespace schoolproject.api.Controllers
{
    [ApiController]
    public class departmentController : appcontrollerbases
    {
        [HttpGet(router.departmentrouting.getdepartmentlist)]
        public async Task<IActionResult> GetDepartmentsListAsync()
        {
            var result = await Mediator.Send(new getdepartmentlistquery());
            return Ok(result);
        }

        [HttpGet(router.departmentrouting.getdepartmentbyid)]
        public async Task<IActionResult> GetDepartmentbyid([FromRoute] int id)
        {
            var result = await Mediator.Send(new getdepartmentbyidquery(id));
            return NewResult(result);
        }

        [HttpGet(router.departmentrouting.getdepartmentpaginited)]
        public async Task<IActionResult> getdepartmentpaginite([FromQuery] getdepartmentpaginatedlistquery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(router.departmentrouting.adddepartment)]
        public async Task<IActionResult> adddepartment([FromBody] adddepartmentcommand department)
        {
            var result = await Mediator.Send(department);
            return NewResult(result);
        }

        [HttpPut(router.departmentrouting.editdepartment)]
        public async Task<IActionResult> editdepartment([FromBody] editdepartment department)
        {
            var result = await Mediator.Send(department);
            return NewResult(result);
        }

        [HttpDelete(router.departmentrouting.deletedepartment)]
        public async Task<IActionResult> deletedepartment([FromRoute] int id)
        {
            var result = await Mediator.Send(new deletedepartmentcommand(id));
            return NewResult(result);
        }
    }
}
