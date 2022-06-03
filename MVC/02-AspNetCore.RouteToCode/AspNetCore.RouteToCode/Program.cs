using AspNetCore.RouteToCode;
using AspNetCore.RouteToCode.API;
using Microsoft.AspNetCore.Http.Json;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(jsonOption =>
{
    jsonOption.SerializerOptions.WriteIndented = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoints =>
{
    //通过ServiceProvider 获取依赖注入Service

    endpoints.ServiceProvider.GetService<ILogger<Program>>();

    //将API封装到特定类中
    UserAPI.Map(endpoints);

    endpoints.MapGet("/hello/{name:alpha}", async context =>
    {
        var name = context.Request.RouteValues["name"];
        await context.Response.WriteAsJsonAsync(new { message = $"Hello {name}" });
    }).RequireAuthorization();

    endpoints.MapPost("/weather", async context =>
    {
        if (!context.Request.HasJsonContentType())
        {
            context.Response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
        }
        var weather = context.Request.ReadFromJsonAsync<WeatherForecast>();
        /*
        更新数据库操作
        await UpdateUserAsync();
        */
        context.Response.StatusCode = (int)HttpStatusCode.Accepted;
    });
});

app.Run();
