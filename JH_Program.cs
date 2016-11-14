using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System.Threading;

namespace AppInsightsTestApplication
{
    class Program
    {

        static void Main(string[] args)
        {
            
            TelemetryConfiguration.Active.TelemetryChannel.DeveloperMode = true;
            TelemetryConfiguration.Active.InstrumentationKey = "6351c81d-798e-44a9-a6b4-875dbec8e325";

            TelemetryClient telemetry = new TelemetryClient();
            telemetry.Context.User.Id = Environment.UserName;
            telemetry.Context.Session.Id = Guid.NewGuid().ToString();
            telemetry.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            telemetry.Context.InstrumentationKey = "6351c81d-798e-44a9-a6b4-875dbec8e325";

            Dictionary<string, string> properties = new Dictionary<string, string>();
            Dictionary<string, double> metrics = new Dictionary<string, double>();

            properties.Add("ApiMethod", "api/Example");
            properties.Add("ResponseCode", "200");
            properties.Add("RequestTime", DateTime.Now.ToShortDateString());

            metrics.Add("RequestDuration", 200);
            metrics.Add("NumberEntriesReturned", 12);
            metrics.Add("DatabaseCallTime", 50);
            metrics.Add("ResponsePrepTime", 150);

            telemetry.TrackRequest("http://example.elend.com/api/example", DateTime.Now, TimeSpan.FromMilliseconds(200), "200", true);
            telemetry.TrackEvent("Tested api/example", properties, metrics);


            Thread.Sleep(1000);
            telemetry.TrackRequest("http://example.elend.com/api/example", DateTime.Now, TimeSpan.FromMilliseconds(500), "404", false);
            properties["ResponseCode"] = "404";
            properties["RequestTime"] = DateTime.Now.ToShortDateString();
            metrics["RequestDuration"] = 500;
            metrics["NumberEntriesReturned"] = 0;
            metrics["DatabaseCallTime"] = 0;
            metrics["ResponsePrepTime"] = 0;
            
            telemetry.TrackEvent("Tested api/example", properties, metrics);

            telemetry.Flush();

        }
    }
}
