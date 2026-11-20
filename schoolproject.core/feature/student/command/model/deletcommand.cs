using MediatR;
using schoolproject.core.bases;

namespace schoolproject.core.feature.student.command.model;

public class deletcommand : IRequest<Response<string>>
{
    public int Id { get; set; }
    
    public deletcommand(int id)
    {
        Id = id;
    }
}