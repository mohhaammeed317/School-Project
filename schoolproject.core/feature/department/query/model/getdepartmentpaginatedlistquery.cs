using MediatR;
using schoolproject.core.feature.department.query.response;
using schoolproject.core.wrappers;
using schoolproject.data.helpers;

namespace schoolproject.core.feature.department.query.model;

public class getdepartmentpaginatedlistquery : IRequest<PaginatedResult<getdepartmentpaginatedlist>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public departmentorderingenum orderby { get; set; }
    public string? search { get; set; }
}
