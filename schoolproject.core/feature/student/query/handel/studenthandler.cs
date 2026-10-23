using MediatR;
using schoolproject.core.feature.student.query.model;
using schoolproject.data.entites;
using schoolproject.service.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace schoolproject.core.feature.student.query.handel
{
    public class studenthandler : IRequestHandler<getstudentlistquery, List<Student>>
    {
        private readonly istudentservice _studentService;

        public studenthandler(istudentservice studentService)
        {
            _studentService = studentService;
        }
        public async Task<List<Student>> Handle(getstudentlistquery request, CancellationToken cancellationToken)
        {
          return await _studentService.GetStudentsListAsync();
        }
    }
}
