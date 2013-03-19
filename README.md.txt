Simple class to let you decorate your methods in the controller with route attributes rather than store all the named routes in global

RouteDecorator example setup
=========================== 

```csharp
//Global.asax
public static void RegisterRoutes(RouteCollection routes)
{
    Assembly.GetExecutingAssembly().RegisterRoutes(routes);
    ...
}

//Some Controller
[RouteDecorator("some/sample/{itemId}", Action = "Some", Controller = "Sample", Name = "SampleItem")]
public ActionResult Sample(string itemId)
{
    return View();
}
```