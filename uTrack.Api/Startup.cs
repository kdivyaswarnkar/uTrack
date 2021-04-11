using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using uTrack.Api.DbConfig;
using uTrack.Business;
using uTrack.Business.Interface;

namespace uTrack.Api
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public static readonly string ApiCorsPolicy = "ApiCorsPloicy";

        public static string[] AllowedOrigins = new string[0];

        public Startup(IConfiguration configuration)
        {
            AllowedOrigins = configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? new string[0];
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddControllers();
            services.AddDatabases(Configuration);
            services.AddCors(options =>
            {
                options.AddPolicy(ApiCorsPolicy, builder =>
                {
                    builder.SetIsOriginAllowed(IsAllowedOrigin)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            DependencyContainer.RegisterService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(ApiCorsPolicy);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static bool IsAllowedOrigin(string host)
        {
            return AllowedOrigins.Any(origin => host.ToLower().EndsWith(origin.Trim()) || host.ToLower().Contains("localhost"));
        }
    }
}
