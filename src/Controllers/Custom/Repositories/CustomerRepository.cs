using ApiTemplate.Modules.Customers.Core;

namespace ApiTemplate.Modules.Customers.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ICollection<Customer> _customers;

    public CustomerRepository()
    {
        _customers = new List<Customer>();
    }

    public Task<Customer> AddAsync(Customer entity, CancellationToken cancellation = default)
    {
        _customers.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<Customer[]> GetAllAsync(CancellationToken cancellation = default) => 
        Task.FromResult(_customers.ToArray());

    public Task<Customer> GetByIdAsync(string id, CancellationToken cancellation = default) =>
        Task.FromResult(_customers.FirstOrDefault(x => x.Uuid == id));
}
