namespace API_Rest.Shared.Definitions
{
    public interface IMetricTracker
    {
        Task<T> TrackAsync<T>(Func<Task<T>> action, string apiVersion, string endPoint);

        Task<T> CircuitBreakerTrackAsync<T>(Func<Task<T>> action, string apiVersion, string endPoint);
    }
}
