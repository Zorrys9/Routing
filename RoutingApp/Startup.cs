using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RoutingApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var routeBuilder = new RouteBuilder(app);

            //routeBuilder.MapRoute("{list=personList}/{person}/{name}/{id?}/{*otherRequest}", async context=> 
            //{
            //    await context.Response.WriteAsync($"hello {context.Request.RouteValues["name"]}");
            //});

            routeBuilder.MapMiddlewareGet("{list=personList}/{person}/{name}/{id?}/{*otherrRequest}", app=> {
                app.Use(async (context, next) =>
                {
                    if (context.Request.RouteValues["name"] != null)
                        await next.Invoke();
                    else
                        await context.Response.WriteAsync("Hello world");
                });
                app.Run(async context =>
                {
                    await context.Response.WriteAsync($"name this person is {context.Request.RouteValues["name"]}");
                });
            });

            app.UseRouter(routeBuilder.Build());

            app.Run(async context =>
            {
                    await context.Response.WriteAsync("hello world");
            });

        }

    }
}
