using MediatR;
using schoolproject.core.bases;

namespace schoolproject.core.feature.department.command.model;

public class deletedepartmentcommand : IRequest<Response<string>>
{
    public int Id { get; set; }

    public deletedepartmentcommand(int id)
    {
        Id = id;
    }
}
