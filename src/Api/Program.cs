using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiSample.Api
{
    public class Program
    {
        private const string DefaultUrl = "http://localhost:80";

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var hostBuilder = WebHost.CreateDefaultBuilder(args);
            hostBuilder.UseStartup<Startup>();
            

            var existingUrls = hostBuilder.GetSetting(WebHostDefaults.ServerUrlsKey)?.Split(';') ?? new string[0];

            var defaultUrl = existingUrls.Length > 0 ? existingUrls[0] : DefaultUrl;
            var parsedDefaultUrl = ServerAddress.FromUrl(defaultUrl);

            var ports = new HashSet<int>(new[] { parsedDefaultUrl.Port });

            var standardHttpPortStr = hostBuilder.GetSetting("HTTP_PORT");
            if (!string.IsNullOrEmpty(standardHttpPortStr))
            {
                var standardHttpPort = Convert.ToInt32(standardHttpPortStr);
                ports.Add(standardHttpPort);
            }

            var standardHttpsPortStr = hostBuilder.GetSetting("HTTPS_PORT");
            if (!string.IsNullOrEmpty(standardHttpsPortStr))
            {
                var standardHttpsPort = Convert.ToInt32(standardHttpsPortStr);
                ports.Add(standardHttpsPort);
            }

            var managementHttpPortStr = Environment.GetEnvironmentVariable("MANAGEMENT_HTTP_PORT");
            if (!string.IsNullOrEmpty(managementHttpPortStr))
            {
                var managementHttpPort = Convert.ToInt32(managementHttpPortStr);
                ports.Add(managementHttpPort);
            }

            hostBuilder.UseUrls(ports.Select(port => $"{parsedDefaultUrl.Scheme}://{parsedDefaultUrl.Host}:{port}").ToArray());
            


            return hostBuilder;

        }
    }
}
