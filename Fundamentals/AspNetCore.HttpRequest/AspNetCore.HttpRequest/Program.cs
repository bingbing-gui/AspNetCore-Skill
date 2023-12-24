using AspNetCore.HttpRequest.GitHub;
using AspNetCore.HttpRequest.Handlers;
using AspNetCore.HttpRequest.Models;
using Microsoft.Net.Http.Headers;
using Polly;
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

#region 

builder.Services.AddScoped<IOperationScoped, OperationScoped>();
builder.Services.AddTransient<OperationHandler>();
builder.Services.AddTransient<OperationResponseHandler>();
//builder.Services.AddScoped<OperationHandler>();
//builder.Services.AddScoped<OperationResponseHandler>();

builder.Services.AddHttpClient("Operation")
    .AddHttpMessageHandler<OperationHandler>()
    .AddHttpMessageHandler<OperationResponseHandler>()
    .SetHandlerLifetime(TimeSpan.FromSeconds(5));

#endregion

#region Polly 
//WaitAndRetryAsync 策略。 请求失败后最多可以重试三次，每次尝试间隔 600 ms
builder.Services.AddHttpClient("PollyAwaitAndRetry")
    .AddTransientHttpErrorPolicy(policyBuilder =>
    policyBuilder.WaitAndRetryAsync(3, retryNumber =>
    TimeSpan.FromMilliseconds(600)));

/*动态选择策略  如果出站请求为 HTTP GET，则应用 10 秒超时。 其他所有 HTTP 方法应用 30 秒超时。 */
var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(10));

var longTimeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(30));

builder.Services.AddHttpClient("PollyDynamic")
    .AddPolicyHandler(policyBuilder => policyBuilder.Method == HttpMethod.Get ? timeoutPolicy : longTimeoutPolicy);

/*添加多个 Polly 处理程序
添加了两个处理程序。
第一个处理程序使用 AddTransientHttpErrorPolicy 添加重试策略。 若请求失败，最多可重试三次。
第二个 AddTransientHttpErrorPolicy 调用添加断路器策略。如果尝试连续失败了 5 次，
    则会阻止后续外部请求 30 秒。断路器策略处于监控状态。通过此客户端进行的所有调用都共享同样的线路状态。 
 */
builder.Services.AddHttpClient("PollyMultiple")
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.RetryAsync(3))
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

/*从 Polly 注册表添加策略*/

var policyRegistry = builder.Services.AddPolicyRegistry();
policyRegistry.Add("Regular", Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)));
policyRegistry.Add("Long", Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30)));

builder.Services.AddHttpClient("PollyRegistryRegular")
    .AddPolicyHandlerFromRegistry("Regular");

builder.Services.AddHttpClient("PollyRegistryLong")
    .AddPolicyHandlerFromRegistry("Long");

#endregion


#region 配置 HttpMessageHandler

builder.Services.AddHttpClient("ConfiguredHttpMessageHandler")
    .ConfigurePrimaryHttpMessageHandler(() =>
        new HttpClientHandler
        {
            AllowAutoRedirect = true,
            UseDefaultCredentials = true
        });

builder.Services.AddHttpClient("NoAutomaticCookies")
    .ConfigurePrimaryHttpMessageHandler(() =>
        new HttpClientHandler
        {
            UseCookies = false
        });
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
