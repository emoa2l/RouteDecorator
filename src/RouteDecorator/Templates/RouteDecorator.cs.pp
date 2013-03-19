using RouteDecorator;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.RouteDecorator), "Start")]

namespace $rootnamespace$.App_Start
{
    public class RouteDecorator
    {

        public static void Start()
        {
            RouteDecorator.Init();
        }

        public static void Init()
        {                        
            foreach (var attr in from type in Assembly.GetExecutingAssembly().GetTypes() 
                                 select type.GetMethods()
                                 .Where(y => Attribute.IsDefined(y, typeof(RouteDecoratorAttribute))).ToList() 
                                 into methods from method in methods 
                                 select method.GetCustomAttributes().OfType<RouteDecoratorAttribute>().ToArray() 
                                 into attributes from attr in attributes 
                                 select attr)
            {
                RouteTable.Routes.MapRoute(
                    name: attr.Name,
                    url: attr.Route,
                    defaults: new { controller = attr.Controller, action = attr.Action }
                    );
            }
        }
    }
}