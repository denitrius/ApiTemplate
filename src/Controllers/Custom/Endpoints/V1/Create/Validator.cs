using FluentValidation;

namespace ApiTemplate.Modules.Customers.Endpoints.V1.Create;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(p => p.Uuid).NotNull().NotEmpty().Length(36);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(p => p.EmailAddress).EmailAddress().When(p => !string.IsNullOrWhiteSpace(p.EmailAddress));
    }
}
