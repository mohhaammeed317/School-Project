using FluentValidation;
using Microsoft.Extensions.Localization;
using schoolproject.core.feature.subject.command.model;
using schoolproject.core.resources;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.subject.command.validation;

public class addsubjectvalidator : AbstractValidator<addsubjectcommand>
{
    private readonly isubjectservice _subjectservice;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public addsubjectvalidator(isubjectservice subjectservice, IStringLocalizer<SharedResources> localizer)
    {
        _subjectservice = subjectservice;
        _localizer = localizer;

        RuleFor(x => x.SubjectNameen).NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.SubjectNamear).NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);

        RuleFor(x => x.SubjectNameen)
            .MustAsync(async (key, cancellationToken) => await _subjectservice.nameexist(key))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]);
        RuleFor(x => x.SubjectNamear)
            .MustAsync(async (key, cancellationToken) => await _subjectservice.nameexist(key))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]);
    }
}
