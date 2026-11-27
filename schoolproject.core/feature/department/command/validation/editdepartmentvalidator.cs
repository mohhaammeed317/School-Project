using FluentValidation;
using Microsoft.Extensions.Localization;
using schoolproject.core.feature.department.command.model;
using schoolproject.core.resources;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.department.command.validation;

public class editdepartmentvalidator : AbstractValidator<editdepartment>
{
    private readonly idepartmentservice _departmentservice;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public editdepartmentvalidator(idepartmentservice departmentservice, IStringLocalizer<SharedResources> localizer)
    {
        _departmentservice = departmentservice;
        _localizer = localizer;

        RuleFor(x => x.DID).NotEmpty().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.DNameen).NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.DNamear).NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);

        RuleFor(x => x.DNameen)
            .MustAsync(async (model, key, cancellationToken) =>
                await _departmentservice.isnameisexistexcludeself(key, model.DID))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]);
        RuleFor(x => x.DNamear)
            .MustAsync(async (model, key, cancellationToken) =>
                await _departmentservice.isnameisexistexcludeself(key, model.DID))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]);
    }
}
