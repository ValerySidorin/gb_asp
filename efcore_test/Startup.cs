using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using efcore_test.Models;
using efcore_test.Services;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace efcore_test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options => 
            {
                options.UseSqlServer(connection);
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                Endpoint endpoint = context.GetEndpoint();

                if (endpoint != null)
                {
                    var route = (endpoint as RouteEndpoint).RoutePattern.RawText;
                    Debug.WriteLine(route);
                    Console.WriteLine(endpoint.DisplayName);
                    Console.WriteLine(route);

                    await next();
                }
                else
                {
                    Debug.WriteLine("No endpoint!");
                    Console.WriteLine("No endpoint!");
                }
                
            });

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapGet("/test", async context =>
                {
                    await context.Response.WriteAsync("Test");
                });
            });
        }
    }
}
