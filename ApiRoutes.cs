﻿using System.Collections.Generic;
using System.Web.Http;
using Orchard.Mvc.Routes;
using Orchard.WebApi.Routes;

namespace XinTuo.Accounts
{
    public class ApiRoutes : IHttpRouteProvider
    {
        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new RouteDescriptor[] {
                new HttpRouteDescriptor {
                    Name="Company Api",
                    Priority=0,
                    RouteTemplate="api/c/{action}/{id}",
                    Defaults=new {
                        area="XinTuo.Accounts",
                        controller="CompanyApi",
                        id=RouteParameter.Optional
                    }
                },
                new HttpRouteDescriptor {
                    Name="Account Api",
                    Priority=1,
                    RouteTemplate="api/a/{action}/{id}",
                    Defaults=new {
                        area="XinTuo.Accounts",
                        controller="AccountApi",
                        id=RouteParameter.Optional
                    }
                },
                new HttpRouteDescriptor {
                    Name="Auxiliary Api",
                    Priority=2,
                    RouteTemplate="api/x/{action}/{id}",
                    Defaults=new {
                        area="XinTuo.Accounts",
                        controller="AuxiliaryApi",
                        id=RouteParameter.Optional
                    }
                }
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach(RouteDescriptor rd in GetRoutes())
            {
                routes.Add(rd);
            }
        }
    }
}