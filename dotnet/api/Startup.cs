using App.Repositories;
using App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    /// <summary>
    ///     The Startup class.
    ///     <para>Contains the startup methods for the application.</para>
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     The Startup class constructor.
        /// </summary>
        /// <param name="configuration">An application configuration.</param>
        public Startup(IConfiguration configuration) => Configuration = configuration;

        /// <summary>
        ///     Gets the Configuration property.
        /// </summary>
        /// <value>An application configuration.</value>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     The ConfigureServices method.
        ///     <para>This method gets called by the runtime. Use this method to add services to the container.</para>
        /// </summary>
        /// <param name="services">A collection of services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IParkingRateService, ParkingRateService>();
            services.AddTransient<IParkingRateRepository, ParkingRateRepository>();

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
            })
            .AddXmlSerializerFormatters()
            .AddXmlDataContractSerializerFormatters();
        }

        /// <summary>
        ///     The Configure method.
        ///     <para>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</para>
        /// </summary>
        /// <param name="app">The application to configure.</param>
        /// <param name="env">Information about the hosting environment.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
