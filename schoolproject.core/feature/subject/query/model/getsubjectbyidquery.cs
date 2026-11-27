using MediatR;
using schoolproject.core.bases;
using schoolproject.core.feature.subject.query.response;

namespace schoolproject.core.feature.subject.query.model;

public class getsubjectbyidquery : IRequest<Response<getsinglesubject>>
{
    public int id { get; set; }

    public getsubjectbyidquery(int id)
    {
        this.id = id;
    }
}
