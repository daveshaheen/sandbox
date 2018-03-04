using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace App
{
    /// <summary>
    ///     The Program class.
    ///     <para>Provides the entry point for the Rates Web API application.</para>
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     The Main method.
        ///     <para>The main entry point for the Rates Web API application.</para>
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args) => BuildWebHost(args).Run();

        /// <summary>
        ///     The BuildWebHost method.
        ///     <para>
        ///         A static method to configure a web host.
        ///     </para>
        ///     <para>
        ///         The web host is configured to use the Kestrel web server and listen on any IPV4 and any IPV6 address on port 5000.
        ///         The listening address and port can be over written with environment variables prefixed with DOTNET_ or with command line parameters.
        ///     </para>
        ///     <para>
        ///         Environment variable usage: DOTNET_URLS=http://*:5000
        ///     </para>
        ///     <para>
        ///         Command line usage. dotnet run --urls http://*:5000
        ///     </para>
        ///     <para>
        ///         The maximum number of connections is unlimited (null) by default.
        ///         The default maximum request body size is 30,000,000 bytes, which is approximately 28.6MB.
        ///         The default minimum rate is 240 bytes/second, with a 5 second grace period.
        ///     </para>
        ///     <para>https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?tabs=aspnetcore2x</para>
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>Returns <see cref="IWebHost" />.</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5000")
                .UseConfiguration(new ConfigurationBuilder()
                    .AddEnvironmentVariables(prefix: "DOTNET_")
                    .AddCommandLine(args)
                    .Build())
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
    }
}
