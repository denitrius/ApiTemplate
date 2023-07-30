using API_Rest.Shared.Definitions;
using Prometheus;
using System.Reflection;

namespace API_Rest.Shared.Services
{
    public sealed class MetricTracker : IMetricTracker
    {
        public const string ProviderResponseTimesMetricName = "requests_duration_seconds";
        public const string ProviderInProgressRequestsMetricName = "requests_in_progress";

        private static readonly string[] _labelNames = new[] { "apiVersion", "endPoint" };
        public static IReadOnlyList<string> LabelNames => Array.AsReadOnly(_labelNames);

        private ICollector<IHistogram> _ResponseTimes;
        private ICollector<IGauge> _InProgressRequests;

        public MetricTracker(IMetricFactory metricFactory = null)
        {
            metricFactory ??= Metrics.WithCustomRegistry(Metrics.DefaultRegistry);

            _ResponseTimes = metricFactory.CreateHistogram(
                ProviderResponseTimesMetricName,
                "Duration histogram of requests processed.",
                new HistogramConfiguration
                {
                    LabelNames = _labelNames,
                });

            _InProgressRequests = metricFactory.CreateGauge(
                ProviderInProgressRequestsMetricName,
                "Number of requests currently in progress.",
                new GaugeConfiguration
                {
                    LabelNames = _labelNames,
                });
        }

        internal MetricTracker(ICollector<IHistogram> providerResponseTimes, ICollector<IGauge> providerInProgressRequests)
        {
            _ResponseTimes = providerResponseTimes;
            _InProgressRequests = providerInProgressRequests;
        }

        public async Task<T> CircuitBreakerTrackAsync<T>(Func<Task<T>> action, string apiVersion, string endPoint)
        {
            using (_InProgressRequests.WithLabels(apiVersion, endPoint).TrackInProgress())
            using (_ResponseTimes.WithLabels(apiVersion, endPoint).NewTimer())
            {
                return await action();
                //return await _circuitBreakerTracker.ExecuteAsync(string.Format("{0}#{1}", providerCode, methodName),
                //    async () => {
                //        var rs = await action();
                //        TiposAgregador.TipoEstadoRespuesta[] timeoutStatus = { TiposAgregador.TipoEstadoRespuesta.timeout, TiposAgregador.TipoEstadoRespuesta.timeout_token_cancelled };
                //        if (timeoutStatus.Contains(rs?.estadoRespuesta?.estado ?? TiposAgregador.TipoEstadoRespuesta.err))
                //            throw new TimeoutException();

                //        return rs;
                //    }
                //    );
            }
        }

        public async Task<T> TrackAsync<T>(Func<Task<T>> action, string apiVersion, string endPoint)
        {
            using (_InProgressRequests.WithLabels(apiVersion, endPoint).TrackInProgress())
            using (_ResponseTimes.WithLabels(apiVersion, endPoint).NewTimer())
            {
                return await action();
            }
        }           
    }
}
