using MediatR;
using schoolproject.core.feature.subject.query.response;
using schoolproject.core.wrappers;
using schoolproject.data.helpers;

namespace schoolproject.core.feature.subject.query.model;

public class getsubjectpaginatedlistquery : IRequest<PaginatedResult<getsubjectpaginatedlist>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public subjectorderingenum orderby { get; set; }
    public string? search { get; set; }
}
