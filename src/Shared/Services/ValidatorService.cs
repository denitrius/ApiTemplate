using FluentValidation;

namespace ApiTemplate.Shared.Services;

public class ValidatorService : IValidatorService
{
    private readonly IServiceProvider _serviceProvider;

    public ValidatorService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ValidateAsync<T>(T model, CancellationToken cancellation = default)
    {
        var validator = _serviceProvider.GetService<IValidator<T>>();

        if (validator is null)
        {
            throw new ValidationException("Validator was not found.");
        }

        var result = await validator.ValidateAsync(model, cancellation);

        if (!result.IsValid)
        {
            throw new ValidationException("Input data is not valid.", result.Errors);
        }
    }
}
