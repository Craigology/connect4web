using System.Configuration;
using Serilog;
using SerilogWeb.Classic.Enrichers;

namespace Connect4
{
    public partial class Startup
    {
        private void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.With<HttpRequestIdEnricher>()
            .Enrich.With<HttpSessionIdEnricher>()
            .WriteTo.Seq(ConfigurationManager.AppSettings["SeqUrl"])
            .CreateLogger();
        }
    }
}