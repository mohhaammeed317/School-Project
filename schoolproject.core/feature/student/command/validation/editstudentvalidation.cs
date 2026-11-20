using FluentValidation;
using schoolproject.core.feature.student.command.model;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.student.command.validation;

public class editstudentvalidation : AbstractValidator<editstudent>
{
    private readonly istudentservice  _studentservice;
    public editstudentvalidation( istudentservice  studentservice)
    {
        this._studentservice = studentservice;

        applyvalidatorrules();
        customvalidation();

    }

    public void applyvalidatorrules()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("name must be not empty")
            .NotNull().WithMessage("name must be not null")
            .MaximumLength(15).WithMessage("name must be less than 15 characters+");

        RuleFor(x => x.Address).NotEmpty().WithMessage("{propertyname} must be not empty")
            .NotNull().WithMessage("{propertyvalue} must be not null")
            .MaximumLength(15).WithMessage("{propertyname} must be less than 15 characters");
    }

    public void customvalidation()
    {
        RuleFor(x=>x.Name).MustAsync(async (model,Key, CancellationToken) =>await _studentservice.isnameisexistexcludeself(Key,model.id))
            .WithMessage("name already exist"); 
    
    }
}