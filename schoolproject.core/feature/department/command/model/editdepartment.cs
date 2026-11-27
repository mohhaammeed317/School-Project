using MediatR;
using schoolproject.core.bases;

namespace schoolproject.core.feature.department.command.model;

public class editdepartment : IRequest<Response<string>>
{
    public int DID { get; set; }
    public string DNameen { get; set; }
    public string DNamear { get; set; }
}
