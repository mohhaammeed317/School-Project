using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using schoolproject.core.bases;
using schoolproject.core.feature.department.command.model;
using schoolproject.core.resources;
using schoolproject.data.entites;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.department.command.handel;

public class departmentcommandhandler : ResponseHandler,
    IRequestHandler<adddepartmentcommand, Response<string>>,
    IRequestHandler<editdepartment, Response<string>>,
    IRequestHandler<deletedepartmentcommand, Response<string>>
{
    private readonly idepartmentservice _departmentService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public departmentcommandhandler(idepartmentservice departmentService, IMapper mapper,
        IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _departmentService = departmentService;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<Response<string>> Handle(adddepartmentcommand request, CancellationToken cancellationToken)
    {
        var department = _mapper.Map<Department>(request);
        var result = await _departmentService.addsync(department);

        if (result == "added successrfully")
            return Success<string>(_localizer[SharedResourcesKey.Success].Value);

        return BadRequest<string>(_localizer[SharedResourcesKey.BadRequest].Value);
    }

    public async Task<Response<string>> Handle(editdepartment request, CancellationToken cancellationToken)
    {
        var department = await _departmentService.GetDepartmentbyidAsync(request.DID);
        if (department == null)
            return NotFound<string>(_localizer[SharedResourcesKey.NotFound].Value);

        _mapper.Map(request, department);
        var result = await _departmentService.updatedepartment(department);

        if (result == "updated successrfully")
            return Success<string>($"{_localizer[SharedResourcesKey.Updated].Value} {department.DID}");

        return BadRequest<string>(_localizer[SharedResourcesKey.BadRequest].Value);
    }

    public async Task<Response<string>> Handle(deletedepartmentcommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentService.GetDepartmentbyidAsync(request.Id);
        if (department == null)
            return NotFound<string>(_localizer[SharedResourcesKey.NotFound].Value);

        var result = await _departmentService.deletedepartment(department);

        if (result == "deleted successrfully")
            return Deleted<string>($"{_localizer[SharedResourcesKey.Deleted].Value} {department.DID}");

        return BadRequest<string>(_localizer[SharedResourcesKey.BadRequest].Value);
    }
}
