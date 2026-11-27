using FluentValidation;
using Microsoft.Extensions.Localization;
using schoolproject.core.feature.subject.command.model;
using schoolproject.core.resources;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.subject.command.validation;

public class deletesubjectvalidator : AbstractValidator<deletesubjectcommand>
{
    private readonly isubjectservice _subjectservice;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public deletesubjectvalidator(isubjectservice subjectservice, IStringLocalizer<SharedResources> localizer)
    {
        _subjectservice = subjectservice;
        _localizer = localizer;

        RuleFor(x => x.Id).NotEmpty().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.Id)
            .MustAsync(async (key, cancellationToken) => await _subjectservice.candelete(key))
            .WithMessage(_localizer[SharedResourcesKey.BadRequest]);
    }
}
