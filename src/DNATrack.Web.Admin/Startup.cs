using DNATrack.Common.Core;
using DNATrack.Common.Messaging;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;

namespace DNATrack.Web.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var rmqSection = Configuration.GetSection(Constants.ConfigSections.Rabbit);
            if (!rmqSection.Exists())
                throw new ConfigurationErrorsException($"Required section '{rmqSection.Path}' not found in configuration");

            var rmqConfig = rmqSection.Get<RabbitMQConfiguration>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions(o =>
                {
                    o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
                });
            
            services
                .Configure<RabbitMQConfiguration>(Configuration.GetSection(Constants.ConfigSections.Rabbit))
                .AddSingleton(Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(rmqConfig.Endpoint), host =>
                {
                    host.Username(rmqConfig.Username);
                    host.Password(rmqConfig.Password);
                });
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            var basePath = Configuration.GetValue<string>("BasePath");
            Console.WriteLine($"using basePath {basePath}");
            app.UsePathBase(basePath);

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
