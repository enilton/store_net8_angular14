using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
config.AddJsonFile("appsettings.json", true, true);
config.AddJsonFile("oncelot.json", true, true);

var services = builder.Services;
services.AddOcelot(config);

services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );
});

var app = builder.Build();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseOcelot().Wait();
app.Run();