using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingApp
{
    public class CustomConstrains :IRouteConstraint
    {
        private string _uri;
        public CustomConstrains(string uri)
        {
            _uri = uri;
        }
        public bool Match(HttpContext context, IRouter route, string routeKey,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            return !(_uri == context.Request.Path);
        }
    }
}
