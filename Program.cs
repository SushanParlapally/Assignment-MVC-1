
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Middleware to terminate chain if URL contains /end
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.Contains("/end"))
    {
        await context.Response.WriteAsync("Chain terminated.");
    }
    else
    {
        await next.Invoke();
    }
});

// Middleware to display hello from middleware 1
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.Contains("/hello"))
    {
        await context.Response.WriteAsync("Hello from middleware 1\n");
        await next.Invoke();
    }
    else
    {
        await next.Invoke();
    }
});

// Middleware to display hello from middleware 2
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.Contains("/hello"))
    {
        await context.Response.WriteAsync("Hello from middleware 2\n");
    }
    else
    {
        await next.Invoke();
    }
});

app.Run();
