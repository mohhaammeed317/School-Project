using MediatR;
using schoolproject.core.bases;
using schoolproject.data.entites;

namespace schoolproject.core.feature.subject.query.model;

public class getsubjectlistquery : IRequest<Response<List<Subjects>>>
{
}
