﻿using System;
using System.Collections.Generic;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Mvc;
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
                    Priority=0,
                    RouteTemplate="api/a/{action}/{id}",
                    Defaults=new {
                        area="XinTuo.Accounts",
                        controller="AccountApi",
                        id=RouteParameter.Optional
                    }
                },
                new HttpRouteDescriptor {
                    Name="Voucher Api",
                    Priority=0,
                    RouteTemplate="api/v/{action}/{id}",
                    Defaults=new {
                        area="XinTuo.Accounts",
                        controller="VoucherApi",
                        id=RouteParameter.Optional
                    }
                }
                //,new RouteDescriptor {Route=new Route("acct/{controller}/{action}/{id}",new RouteValueDictionary { { "area" , "XinTuo.Accounts" }},new MvcRouteHandler()) }
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