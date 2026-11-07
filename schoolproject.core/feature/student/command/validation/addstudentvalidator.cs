using FluentValidation;
using schoolproject.core.feature.student.command.model;

namespace schoolproject.core.feature.student.command.validation;

public class addstudentvalidator : AbstractValidator<addstudentcommand>
{
    public addstudentvalidator()
    {
        
        
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
    
}