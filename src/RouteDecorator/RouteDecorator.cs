using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace RouteDecorator
{
    public static class RouteDecorator
    {
        /// <summary>
        /// Gets a list of methods to assign routes for from the specified assembly
        /// </summary>
        /// <param name="asm">The assembly in which the routes are defined.</param>
        /// <param name="routes">MVC Route collection.</param>
        public static void RegisterRoutes(this Assembly asm, System.Web.Routing.RouteCollection routes)
        {
            var types = asm.GetTypes();

            foreach (var type in types)
            {
                var methods =
                    type.GetMethods().Where(y => Attribute.IsDefined(y, typeof (RouteDecoratorAttribute))).ToList();
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes().OfType<RouteDecoratorAttribute>().ToArray();
                    attributes.RegisterRoutes(routes);
                }
            }
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="attributes">attribute collection.</param>
        /// <param name="routes">MVC Route collection.</param>
        public static void RegisterRoutes(this RouteDecoratorAttribute[] attributes, System.Web.Routing.RouteCollection routes)
        {
            foreach (var attr in attributes)
            {
                attr.Getroute(routes);
            }
        }
    }
}
