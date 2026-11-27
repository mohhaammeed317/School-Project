using MediatR;
using schoolproject.core.bases;
using schoolproject.core.feature.department.query.response;

namespace schoolproject.core.feature.department.query.model;

public class getdepartmentbyidquery : IRequest<Response<getsingledepartment>>
{
    public int id { get; set; }

    public getdepartmentbyidquery(int id)
    {
        this.id = id;
    }
}
