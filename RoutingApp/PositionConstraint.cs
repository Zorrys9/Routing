using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingApp
{
    public class PositionConstraint : IRouteConstraint
    {
        string[] positions = new[] { "adminlist", "userlist", "administratorlist" };

        public bool Match(HttpContext context, IRouter route, string routeKey,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            return positions.Contains(values[routeKey]?.ToString().ToLowerInvariant());
        }
    }
}
