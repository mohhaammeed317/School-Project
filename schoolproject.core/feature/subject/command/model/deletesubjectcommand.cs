using MediatR;
using schoolproject.core.bases;

namespace schoolproject.core.feature.subject.command.model;

public class deletesubjectcommand : IRequest<Response<string>>
{
    public int Id { get; set; }

    public deletesubjectcommand(int id)
    {
        Id = id;
    }
}
