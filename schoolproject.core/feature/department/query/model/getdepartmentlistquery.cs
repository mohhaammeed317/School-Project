using MediatR;
using schoolproject.core.bases;
using schoolproject.data.entites;

namespace schoolproject.core.feature.department.query.model;

public class getdepartmentlistquery : IRequest<Response<List<Department>>>
{
}
