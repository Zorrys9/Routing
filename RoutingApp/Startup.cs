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
using Microsoft.Extensions.Options;

namespace RoutingApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("position", typeof(PositionConstraint));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var RouteHandler = new RouteHandler(HandleAsync);

            var routeBuilder = new RouteBuilder(app, RouteHandler);

            routeBuilder.Routes.Add(new ManagerRoute());

            routeBuilder.MapRoute(
                name:"myMap",
                template:"{personList:position:regex(^*.list.*)}/{person}/{name}-age{age}/{id?}",
                defaults: new {person = "person" },
                new { myConstrain = new CustomConstrains("/GetlistUser/person/alex-age19/19")});

            routeBuilder.MapRoute(
                name: "adminMap",
                template: "{id:int}/{name}/{*otherPages}");

            app.UseRouter(routeBuilder.Build());

            app.Run(async context =>
            {
                    await context.Response.WriteAsync("hello world");
            });

        }
        public async Task HandleAsync(HttpContext context)
        {
            var values = context.GetRouteData().Values;

            string name = values["name"].ToString();
            string age = values["age"].ToString();
            await context.Response.WriteAsync($"name: {name}, age: {age}");
        }

    }
}
