using System.IO;
using App.Repositories;
using App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace App
{
    /// <summary>
    ///     The Startup class.
    ///     <para>Contains the startup methods for the application.</para>
    /// </summary>
    public class Startup
    {
        private const string DocsVersion = "v1";
        private const string DocsName = "Parking Rates Web API";

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

            services.AddMvcCore()
                .AddApiExplorer();

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
            })
            .AddXmlSerializerFormatters()
            .AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc(DocsVersion, new Info
                {
                    Title = DocsName,
                    Version = DocsVersion
                });

                s.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/docs/App.xml"));
            });
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
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.RoutePrefix = "";
                s.SwaggerEndpoint($"/swagger/{DocsVersion}/swagger.json", DocsName);
            });
        }
    }
}
