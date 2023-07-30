namespace ApiTemplate.Shared.Definitions;

public interface IValidatorService
{
    Task ValidateAsync<T>(T model, CancellationToken cancellation = default);
}
