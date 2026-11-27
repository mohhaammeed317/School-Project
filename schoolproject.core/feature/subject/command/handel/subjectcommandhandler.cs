using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using schoolproject.core.bases;
using schoolproject.core.feature.subject.command.model;
using schoolproject.core.resources;
using schoolproject.data.entites;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.subject.command.handel;

public class subjectcommandhandler : ResponseHandler,
    IRequestHandler<addsubjectcommand, Response<string>>,
    IRequestHandler<editsubject, Response<string>>,
    IRequestHandler<deletesubjectcommand, Response<string>>
{
    private readonly isubjectservice _subjectService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public subjectcommandhandler(isubjectservice subjectService, IMapper mapper,
        IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _subjectService = subjectService;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<Response<string>> Handle(addsubjectcommand request, CancellationToken cancellationToken)
    {
        var subject = _mapper.Map<Subjects>(request);
        var result = await _subjectService.addsync(subject);

        if (result == "added successrfully")
            return Success<string>(_localizer[SharedResourcesKey.Success].Value);

        return BadRequest<string>(_localizer[SharedResourcesKey.BadRequest].Value);
    }

    public async Task<Response<string>> Handle(editsubject request, CancellationToken cancellationToken)
    {
        var subject = await _subjectService.GetSubjectbyidAsync(request.SubID);
        if (subject == null)
            return NotFound<string>(_localizer[SharedResourcesKey.NotFound].Value);

        _mapper.Map(request, subject);
        var result = await _subjectService.updatesubject(subject);

        if (result == "updated successrfully")
            return Success<string>($"{_localizer[SharedResourcesKey.Updated].Value} {subject.SubID}");

        return BadRequest<string>(_localizer[SharedResourcesKey.BadRequest].Value);
    }

    public async Task<Response<string>> Handle(deletesubjectcommand request, CancellationToken cancellationToken)
    {
        var subject = await _subjectService.GetSubjectbyidAsync(request.Id);
        if (subject == null)
            return NotFound<string>(_localizer[SharedResourcesKey.NotFound].Value);

        var result = await _subjectService.deletesubject(subject);

        if (result == "deleted successrfully")
            return Deleted<string>($"{_localizer[SharedResourcesKey.Deleted].Value} {subject.SubID}");

        return BadRequest<string>(_localizer[SharedResourcesKey.BadRequest].Value);
    }
}
