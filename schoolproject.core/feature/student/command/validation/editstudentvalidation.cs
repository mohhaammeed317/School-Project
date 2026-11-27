using FluentValidation;
using Microsoft.Extensions.Localization;
using schoolproject.core.feature.student.command.model;
using schoolproject.core.resources;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.student.command.validation;

public class editstudentvalidation : AbstractValidator<editstudent>
{
    private readonly istudentservice  _studentservice;
    private readonly IStringLocalizer<SharedResources> _localizer;
    public editstudentvalidation( istudentservice  studentservice, IStringLocalizer<SharedResources> localizer)
    {
        this._studentservice = studentservice;
        this._localizer = localizer;

        applyvalidatorrules();
        customvalidation();

    }

    public void applyvalidatorrules()
    {
        RuleFor(x => x.Nameen).NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required])
            .MaximumLength(15).WithMessage(_localizer[SharedResourcesKey.MaxLengthis15]);

        RuleFor(x => x.Addressen).NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required])
            .MaximumLength(15).WithMessage(_localizer[SharedResourcesKey.MaxLengthis15]);
        RuleFor(x => x.Namear).NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required])
            .MaximumLength(15).WithMessage(_localizer[SharedResourcesKey.MaxLengthis15]);

        RuleFor(x => x.Addressar).NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required])
            .MaximumLength(15).WithMessage(_localizer[SharedResourcesKey.MaxLengthis15]);
    }

    public void customvalidation()
    {
        RuleFor(x=>x.Namear).MustAsync(async (model,Key, CancellationToken) =>await _studentservice.isnameisexistexcludeself(Key,model.id))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]); 
        RuleFor(x=>x.Nameen).MustAsync(async (model,Key, CancellationToken) =>await _studentservice.isnameisexistexcludeself(Key,model.id))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]); 
    
    }
}
