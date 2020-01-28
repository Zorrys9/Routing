using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace RoutingApp
{
    public class ManagerRoute : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }
        public async Task RouteAsync(RouteContext context)
        {
            string url = context.HttpContext.Request.Path.Value.TrimEnd('/');
            if(url.StartsWith("/Manager", StringComparison.OrdinalIgnoreCase))
            {
                context.Handler = async ctx =>
                {
                    await ctx.Response.WriteAsync("Hello manager");
                };
            }
            await Task.CompletedTask;
        }
    }
}
