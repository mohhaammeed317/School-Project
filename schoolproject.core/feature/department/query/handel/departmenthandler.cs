using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using schoolproject.core.bases;
using schoolproject.core.feature.department.query.model;
using schoolproject.core.feature.department.query.response;
using schoolproject.core.resources;
using schoolproject.core.wrappers;
using schoolproject.data.entites;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.department.query.handel
{
    public class departmenthandler : ResponseHandler,
        IRequestHandler<getdepartmentlistquery, Response<List<Department>>>,
        IRequestHandler<getdepartmentbyidquery, Response<getsingledepartment>>,
        IRequestHandler<getdepartmentpaginatedlistquery, PaginatedResult<getdepartmentpaginatedlist>>
    {
        private readonly idepartmentservice _departmentService;
        private readonly IMapper _mapper;

        public departmenthandler(idepartmentservice departmentService, IMapper mapper,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        public async Task<Response<List<Department>>> Handle(getdepartmentlistquery request,
            CancellationToken cancellationToken)
        {
            var departments = await _departmentService.GetDepartmentsListAsync();
            return Success(departments);
        }

        public async Task<Response<getsingledepartment>> Handle(getdepartmentbyidquery request,
            CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentbyidAsync(request.id);
            if (department == null)
                return NotFound<getsingledepartment>();

            return Success(_mapper.Map<getsingledepartment>(department));
        }

        public async Task<PaginatedResult<getdepartmentpaginatedlist>> Handle(getdepartmentpaginatedlistquery request,
            CancellationToken cancellationToken)
        {
            var filtered = _departmentService.filterdepartmentpaginatedquerable(request.orderby, request.search);

            var page = await filtered
                .Select(x => new { x.DID, x.DNameen, x.DNamear })
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            var data = page.Data
                .Select(x => new getdepartmentpaginatedlist(
                    x.DID,
                    new Department().Localize(x.DNameen, x.DNamear)))
                .ToList();

            return PaginatedResult<getdepartmentpaginatedlist>.Success(
                data, page.TotalCount, page.CurrentPage, page.PageSize);
        }
    }
}
