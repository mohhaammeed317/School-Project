using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using schoolproject.core.bases;
using schoolproject.core.feature.subject.query.model;
using schoolproject.core.feature.subject.query.response;
using schoolproject.core.resources;
using schoolproject.core.wrappers;
using schoolproject.data.entites;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.subject.query.handel
{
    public class subjecthandler : ResponseHandler,
        IRequestHandler<getsubjectlistquery, Response<List<Subjects>>>,
        IRequestHandler<getsubjectbyidquery, Response<getsinglesubject>>,
        IRequestHandler<getsubjectpaginatedlistquery, PaginatedResult<getsubjectpaginatedlist>>
    {
        private readonly isubjectservice _subjectService;
        private readonly IMapper _mapper;

        public subjecthandler(isubjectservice subjectService, IMapper mapper,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }

        public async Task<Response<List<Subjects>>> Handle(getsubjectlistquery request,
            CancellationToken cancellationToken)
        {
            var subjects = await _subjectService.GetSubjectsListAsync();
            return Success(subjects);
        }

        public async Task<Response<getsinglesubject>> Handle(getsubjectbyidquery request,
            CancellationToken cancellationToken)
        {
            var subject = await _subjectService.GetSubjectbyidAsync(request.id);
            if (subject == null)
                return NotFound<getsinglesubject>();

            return Success(_mapper.Map<getsinglesubject>(subject));
        }

        public async Task<PaginatedResult<getsubjectpaginatedlist>> Handle(getsubjectpaginatedlistquery request,
            CancellationToken cancellationToken)
        {
            var filtered = _subjectService.filtersubjectpaginatedquerable(request.orderby, request.search);

            var page = await filtered
                .Select(x => new { x.SubID, x.SubjectNameen, x.SubjectNamear, x.Period })
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            var data = page.Data
                .Select(x => new getsubjectpaginatedlist(
                    x.SubID,
                    new Subjects().Localize(x.SubjectNameen, x.SubjectNamear),
                    x.Period))
                .ToList();

            return PaginatedResult<getsubjectpaginatedlist>.Success(
                data, page.TotalCount, page.CurrentPage, page.PageSize);
        }
    }
}
