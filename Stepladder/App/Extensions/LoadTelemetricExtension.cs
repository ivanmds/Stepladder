using App.Settings;
using Bankly.Sdk.Opentelemetry.Configuration;
using Bankly.Sdk.Opentelemetry.Extensions;
using OpenTelemetry.Trace;

namespace App.Extensions
{
    public static class LoadTelemetricExtension
    {
        public static void AddTelemetric(this WebApplicationBuilder builder)
        {
            var appSetting = ApplicationSetting.Current;

            if (appSetting.Startup.EnableTelemetry)
            {
                var otelConfig = new OpenTelemetryConfig
                {
                    ServiceName = appSetting.Startup.ServiceName,
                    ServiceVersion = appSetting.Startup.ServiceVersion,
                    Endpoint = appSetting.Startup.OtelEndpoint,
                    IsGrpc = false,
                    EnableConsoleExporter = true
                };

                builder.Logging.AddBanklyOpenTelemetryLogging(otelConfig);
                builder.Services.AddBanklyOpenTelemetryMetrics(otelConfig);

                var tracing = builder.Services.AddBanklyOpenTelemetryTracing(otelConfig);
                tracing.AddRedisInstrumentation();
            }
        }
    }
}
