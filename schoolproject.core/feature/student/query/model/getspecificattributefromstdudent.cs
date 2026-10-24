using MediatR;
using schoolproject.core.feature.student.query.response;
using System;
using System.Collections.Generic;
using System.Text;
using schoolproject.core.bases;

namespace schoolproject.core.feature.student.query.model
{
    public class getspecificattributefromstdudent : IRequest<Response<List<getspecificattributefromstd>>>
    {
    }
}
