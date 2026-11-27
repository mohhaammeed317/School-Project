using MediatR;
using schoolproject.core.bases;

namespace schoolproject.core.feature.department.command.model;

public class adddepartmentcommand : IRequest<Response<string>>
{
    public string DNameen { get; set; }
    public string DNamear { get; set; }
}
