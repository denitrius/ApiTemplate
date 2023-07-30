namespace ApiTemplate.Shared.Definitions;

public interface IRepository<T> where T : class
{
    Task<T[]> GetAllAsync(CancellationToken cancellation = default);

    Task<T> GetByIdAsync(string id, CancellationToken cancellation = default);

    Task<T> AddAsync(T entity, CancellationToken cancellation = default);
}
