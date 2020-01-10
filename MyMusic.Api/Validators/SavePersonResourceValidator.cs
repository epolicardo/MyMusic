using FluentValidation;
using MyMusic.Api.Resources;

namespace MyMusic.Api.Validations
{
    public class SavePersonResourceValidator : AbstractValidator<SavePersonResource>
    {
        public SavePersonResourceValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(50);
            
            RuleFor(a => a.Lastname)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}