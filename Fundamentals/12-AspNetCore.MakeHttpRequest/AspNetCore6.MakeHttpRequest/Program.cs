using AspNetCore6.MakeHttpRequest.GitHub;
using AspNetCore6.MakeHttpRequest.Handlers;
using Microsoft.Net.Http.Headers;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ValidateHeaderHandler>();
#region Basic usage
//Add services to the container.
//无法添加.AddHttpMessageHandler<ValidateHeaderHandler>() 方法,
//因为该方法返回IServiceCollection
builder.Services.AddHttpClient();
#endregion
// Add services to the container.
builder.Services.AddRazorPages();

#region NamedClients

builder.Services.AddHttpClient("GitHub", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.github.com/");
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
    httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");
});
#endregion

#region ClientTyped
//给GitHubService增加 DelegatingHandler 处理
builder.Services.AddHttpClient<GitHubService>()
    .AddHttpMessageHandler<ValidateHeaderHandler>();

#endregion

#region RefitClient
builder.Services.AddRefitClient<IGitHubClient>()
    .ConfigureHttpClient(httpClient =>
    {
        httpClient.BaseAddress = new Uri("https://api.github.com/");
        httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
        httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");
    });
#endregion

#region HttpMessageHandlerMultiple

builder.Services.AddTransient<SampleHandler1>();
builder.Services.AddTransient<SampleHandler2>();

builder.Services.AddHttpClient("MultipleHttpMessageHandlers")
    .AddHttpMessageHandler<SampleHandler1>()
    .AddHttpMessageHandler<SampleHandler2>();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
