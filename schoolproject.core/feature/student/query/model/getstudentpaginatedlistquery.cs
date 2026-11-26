using MediatR;
using schoolproject.core.feature.student.query.response;
using schoolproject.core.wrappers;
using schoolproject.data.entites;

namespace schoolproject.core.feature.student.query.model;

public class getstudentpaginatedlistquery : IRequest<PaginatedResult<getstudentpaginatedlist>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string[]? orderby { get; set; }
    public string? search { get; set; }

   /* public getstudentpaginatedlistquery(int pageNumber, int pageSize, string[]? orderby = null, string? search = null)
    {
           this.PageNumber = pageNumber;    
           this.PageSize = pageSize;
           this.orderby = orderby;
           this.search = search;
    }*/
}