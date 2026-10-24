using AutoMapper;
using MediatR;
using schoolproject.core.feature.student.query.model;
using schoolproject.core.feature.student.query.response;
using schoolproject.data.entites;
using schoolproject.service.abstracts;
using schoolproject.core.bases;
using System;
using System.Collections.Generic;
using System.Text;


namespace schoolproject.core.feature.student.query.handel
{
    public class studenthandler : ResponseHandler, IRequestHandler<getstudentlistquery, Response<List<Student>>>
                                        ,IRequestHandler<getstudentbyidquery, Response< Student>>, IRequestHandler<getspecificattributefromstdudent, Response< List<getspecificattributefromstd>>>
    {
        private readonly istudentservice _studentService;
        private readonly IMapper _mapper;

        public studenthandler(istudentservice studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        public async Task <Response<List<Student>>> Handle(getstudentlistquery request, CancellationToken cancellationToken)
        {
            var stdlist =await _studentService.GetStudentsListAsync();
            return Success(stdlist);
        }

        public async Task<Response<Student>> Handle(getstudentbyidquery request, CancellationToken cancellationToken)
        {
            var stdlist =await _studentService.GetStudentbyidtAsync(request.id);
                return Success(stdlist);
        }

        public async Task<Response<List<getspecificattributefromstd>>> Handle(getspecificattributefromstdudent request, CancellationToken cancellationToken)
        {
            var stdlist = await _studentService.GetStudentbyarrAsync();
            ///map(source,destination)or map<des>(source)
            /// 
            var stdlistmapper = _mapper.Map<List<getspecificattributefromstd>>(stdlist);
            return Success(stdlistmapper);
        }



        /*public async Task<List<getspecificattributefromstd>> Handle(getspecificattributefromstdudent request, CancellationToken cancellationToken)
        {
            var stdlist = await _studentService.GetStudentsListAsync();
            ///map(source,destination)or map<des>(source)
            ///
            var stdlistmapper = _mapper.Map<List<getspecificattributefromstd>>(stdlist);
            return stdlistmapper;
        }



        /* public async Task<List<getspecificattributefromstd>> Handle(getspecificattributefromstdudent request, CancellationToken cancellationToken)
{
    var stdlist = await _studentService.GetStudentsListAsync();
    ///map(source,destination)or map<des>(source)
    ///
    var stdlistmapper = _mapper.Map<List<getspecificattributefromstd>>(stdlist);
    return stdlistmapper;

}*/

    }
}
