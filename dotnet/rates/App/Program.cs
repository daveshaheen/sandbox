using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;

namespace App {
    /// <summary>
    /// Program
    /// <para>
    /// Provides the entry point for the Rates Web API application.
    /// </para>
    /// <para>
    /// Currently there is only one route that can be can be accessed at: http://*::5000/api/{version}/parking/rates.{format}?start={start}&amp;end={end}.
    /// </para>
    /// <para>
    /// The {version} can be anything right now. It's there so it's available incase of future updates.
    /// </para>
    /// <para>
    /// The .{format} is optional and can either be xml, json, proto, or left off. Note: proto does not currently work.
    /// </para>
    /// <para>
    /// The {start} and {end} must be on the same day and in ISO 8601 format. For example <c>start=2015-07-01T07:00:00Z</c> and <c>end=2015-07-01T12:00:00Z</c>.
    /// The endpoint will either return a price as a 32 bit int, a bad request, or a 404 with the message unavailable.
    /// </para>
    /// </summary>
    public class Program {
        /// <summary>
        /// Main
        /// <para>The main entry point for the Rates Web API application.</para>
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static int Main(string[] args) {
            var elasticSearchUrl = Environment.GetEnvironmentVariable("DOTNET_ELASTICSEARCH_URL");
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext();

            if (elasticSearchUrl.Length > 0) {
                loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri($"{elasticSearchUrl}")) {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true)
                });
            }

            Log.Logger = loggerConfiguration
                .WriteTo.File(new CompactJsonFormatter(), "./logs/log.json", buffered : true, rollingInterval : RollingInterval.Day)
                .CreateLogger();

            try {
                Log.Information("Starting web host");
                BuildWebHost(args).Run();
                return 0;
            } catch (Exception ex) {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            } finally {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// BuildWebHost
        /// <para>
        /// A static method to configure a web host.
        /// </para>
        /// <para>
        /// The web host is configured to use the Kestrel web server and listen on any IPV4 and any IPV6 address on port 5000.
        /// The listening address and port can be over written with environment variables prefixed with DOTNET_ or with command line parameters.
        /// </para>
        /// <para>
        /// Environment variable usage: DOTNET_URLS=http://*:5000
        /// </para>
        /// <para>
        /// Command line usage. dotnet run --urls http://*:5000
        /// </para>
        /// <para>
        /// The maximum number of connections is unlimited (null) by default.
        /// The default maximum request body size is 30,000,000 bytes, which is approximately 28.6MB.
        /// The default minimum rate is 240 bytes/second, with a 5 second grace period.
        /// </para>
        /// <para>https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?tabs=aspnetcore2x</para>
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>Returns <see cref="IWebHost" />.</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseUrls("http://*:5000")
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseConfiguration(new ConfigurationBuilder()
                .AddEnvironmentVariables(prefix: "DOTNET_")
                .AddCommandLine(args)
                .Build())
            .UseKestrel()
            .UseStartup<Startup>()
            .UseSerilog()
            .Build();
    }
}
