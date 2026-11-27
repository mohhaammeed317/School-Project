using FluentValidation;
using Microsoft.Extensions.Localization;
using schoolproject.core.feature.department.command.model;
using schoolproject.core.resources;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.department.command.validation;

public class adddepartmentvalidator : AbstractValidator<adddepartmentcommand>
{
    private readonly idepartmentservice _departmentservice;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public adddepartmentvalidator(idepartmentservice departmentservice, IStringLocalizer<SharedResources> localizer)
    {
        _departmentservice = departmentservice;
        _localizer = localizer;

        RuleFor(x => x.DNameen).NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.DNamear).NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);

        RuleFor(x => x.DNameen)
            .MustAsync(async (key, cancellationToken) => await _departmentservice.nameexist(key))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]);
        RuleFor(x => x.DNamear)
            .MustAsync(async (key, cancellationToken) => await _departmentservice.nameexist(key))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]);
    }
}
