namespace ApiTemplate.Modules.Customers.Endpoints.V1.GetById;

public record Response
{
    public string Uuid { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    public string State { get; set; }
}
