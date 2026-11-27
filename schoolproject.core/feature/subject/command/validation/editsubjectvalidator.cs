using FluentValidation;
using Microsoft.Extensions.Localization;
using schoolproject.core.feature.subject.command.model;
using schoolproject.core.resources;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.subject.command.validation;

public class editsubjectvalidator : AbstractValidator<editsubject>
{
    private readonly isubjectservice _subjectservice;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public editsubjectvalidator(isubjectservice subjectservice, IStringLocalizer<SharedResources> localizer)
    {
        _subjectservice = subjectservice;
        _localizer = localizer;

        RuleFor(x => x.SubID).NotEmpty().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.SubjectNameen).NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.SubjectNamear).NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);

        RuleFor(x => x.SubjectNameen)
            .MustAsync(async (model, key, cancellationToken) =>
                await _subjectservice.isnameisexistexcludeself(key, model.SubID))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]);
        RuleFor(x => x.SubjectNamear)
            .MustAsync(async (model, key, cancellationToken) =>
                await _subjectservice.isnameisexistexcludeself(key, model.SubID))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]);
    }
}
