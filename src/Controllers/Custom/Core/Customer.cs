using System.Text.Json.Serialization;

namespace ApiTemplate.Modules.Customers.Core;

public record Customer
{
    [JsonPropertyName("id")]
    public string Uuid { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string EmailAddress { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public State State { get; init; } = Core.State.INACTIVE;
}