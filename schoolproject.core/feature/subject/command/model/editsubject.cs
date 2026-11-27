using MediatR;
using schoolproject.core.bases;

namespace schoolproject.core.feature.subject.command.model;

public class editsubject : IRequest<Response<string>>
{
    public int SubID { get; set; }
    public string SubjectNameen { get; set; }
    public string SubjectNamear { get; set; }
    public DateTime Period { get; set; }
}
