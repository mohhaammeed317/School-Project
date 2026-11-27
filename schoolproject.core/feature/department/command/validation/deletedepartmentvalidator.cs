using FluentValidation;
using Microsoft.Extensions.Localization;
using schoolproject.core.feature.department.command.model;
using schoolproject.core.resources;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.department.command.validation;

public class deletedepartmentvalidator : AbstractValidator<deletedepartmentcommand>
{
    private readonly idepartmentservice _departmentservice;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public deletedepartmentvalidator(idepartmentservice departmentservice, IStringLocalizer<SharedResources> localizer)
    {
        _departmentservice = departmentservice;
        _localizer = localizer;

        RuleFor(x => x.Id).NotEmpty().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.Id)
            .MustAsync(async (key, cancellationToken) => await _departmentservice.candelete(key))
            .WithMessage(_localizer[SharedResourcesKey.BadRequest]);
    }
}
