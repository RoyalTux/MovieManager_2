using CorrelationId;
using CorrelationId.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Versioning;
using MovieManager.Api.Middleware;
using MovieManager.Api.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(t =>
{
    t.ApiVersionReader = new UrlSegmentApiVersionReader();
    t.ReportApiVersions = true;
});
builder.Services.AddSwaggerGen();
builder.Services.AddCore(builder.Configuration);
builder.Services.AddLogging();
builder.Services.AddDefaultCorrelationId();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "Movie Manager API V1");
        c.OAuthAppName("Movie Manager API");
    });
}

app.UseCorrelationId();

app.UseMiddleware<HealthCheckMiddleware>();

app.UseMiddleware<GlobalExceptionMiddleware>();

//app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
