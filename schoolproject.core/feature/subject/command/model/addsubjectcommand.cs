using MediatR;
using schoolproject.core.bases;

namespace schoolproject.core.feature.subject.command.model;

public class addsubjectcommand : IRequest<Response<string>>
{
    public string SubjectNameen { get; set; }
    public string SubjectNamear { get; set; }
    public DateTime Period { get; set; }
}
