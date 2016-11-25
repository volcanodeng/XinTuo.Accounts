using System.Collections.Generic;
using System.Web.Routing;
using System.Web.Mvc;
using Orchard.Mvc.Routes;

namespace XinTuo.Accounts
{
    public class MvcRoutes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
                new RouteDescriptor {
                    Priority = 5,
                    Route =new Route("web/acct/{action}/{id}",
                                        new RouteValueDictionary {
                                            {"area", "XinTuo.Accounts"},
                                            {"controller", "Account"}
                                        },
                                        null,
                                        new RouteValueDictionary {
                                            { "area" , "XinTuo.Accounts" }
                                        },
                                        new MvcRouteHandler())
                }
            };
        }
    }
}