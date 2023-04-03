using FluentValidation;
using StructureMap;

namespace ContributionTracker.Validators
{
    public class TransactionDtoValidator : AbstractValidator<TransactionDto>
    {
        public TransactionDtoValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().NotNull().WithMessage("Enter a number");
            RuleFor(x => x.Amount)
                .Custom((x, context) =>
                {
                    if ((!(int.TryParse(x, out int value))))
                    {
                        context.AddFailure($"{x} is not a valid number");
                    }
                });
            RuleFor(x => x.PayeeName).NotEmpty().NotNull().WithMessage("Enter your name");
        }
    }
}
