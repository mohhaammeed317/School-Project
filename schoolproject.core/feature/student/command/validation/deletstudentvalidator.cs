using FluentValidation;
using schoolproject.core.feature.student.command.model;
using schoolproject.service.abstracts;

namespace schoolproject.core.feature.student.command.validation;

public class deletstudentvalidator : AbstractValidator<deletcommand>
{
    
    private readonly istudentservice  _studentservice;
    public deletstudentvalidator( istudentservice  studentservice)
    {
        this._studentservice = studentservice;

        applyvalidatorrules();
        customvalidation();

    }

    public void applyvalidatorrules()
    {
        RuleFor(X => X.Id).NotEmpty().WithMessage("ID is required");


    }

    public void customvalidation()
    {
        RuleFor(x=>x.Id).MustAsync(async (Key, CancellationToken) =>await _studentservice.checkondidusingid(Key))
            .WithMessage("DID IS EXIST");
    
    }
}