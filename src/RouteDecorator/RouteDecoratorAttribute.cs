using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RouteDecorator
{
    /// <summary>
    /// Decorate a method with a declarative route
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method, 
        AllowMultiple = true, 
        Inherited = true)]
    public class RouteDecoratorAttribute : Attribute
    {
        private readonly string _route;
        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <value>
        /// The route.
        /// </value>
        public string Route { get { return _route; } }

        /// <summary>
        /// Gets or sets the route name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        private string _controller;
        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        public string Controller {
            get { return InferController(); }
            set { _controller = value; }
        }

        private string _action;
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action
        {
            get { return InferAction(); }
            set { _action = value; }
        }

        private string[] _routeParts;
        /// <summary>
        /// Gets the route parts. separated by /
        /// </summary>
        /// <value>
        /// The route parts.
        /// </value>
        private IEnumerable<string> RouteParts
        {
            get { return _routeParts ?? (_routeParts = _route.Split('/')); }
        }

        public RouteDecoratorAttribute(string route)
        {
            this._route = route;
        }

        /// <summary>
        /// adds the defined route to the passed route collection
        /// </summary>
        /// <param name="routes">MVC Route collection</param>
        public void Getroute(RouteCollection routes)
        {            
            routes.MapRoute(
                    Name,
                    Route,
                    new { controller = Controller, action = Action });
        }

        /// <summary>
        /// Infers the controller.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoControllerException"></exception>
        private string InferController()
        {
            if (!string.IsNullOrEmpty(_controller)) return _controller;
            
            var controllerPart = RouteParts.FirstOrDefault(t => t.StartsWith("{"));
            if (controllerPart == null)
            {
                throw new NoControllerException();
            }
            else
            {
                var result = controllerPart.TrimStart('{').TrimEnd('}');
                return result;
            }
        }

        /// <summary>
        /// Infers the action.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoActionException"></exception>
        private string InferAction()
        {
            if (!string.IsNullOrEmpty(_action)) return _action;

            var actionParts = RouteParts.Where(t => t.StartsWith("{")).ToList();
            if (actionParts == null || actionParts.Count() > 2)
            {
                throw new NoActionException();
            }
            else
            {
                var result = actionParts[1].TrimStart('{').TrimEnd('}');
                return result;
            }
        }
    }
}
