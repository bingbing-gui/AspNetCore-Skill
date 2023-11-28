#define CORS_policy_options

#if CORS_with_default_policy_and_middleware
#region
var builder = WebApplication.CreateBuilder(args);
/*
 * CORS with default policy and middleware
 * 添加跨域请求
 */
builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(
        policy => 
        {
            policy.WithOrigins("http://example.com",
                "http://www.contoso.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
        ); ; 


});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//添加跨域请求
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
#endregion
#elif CORS_with_named_policy_and_middleware
#region
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
/*
 * CORS_with_named_policy_and_middleware
 * 添加跨域请求
 */
builder.Services.AddCors(options => 
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy => 
        {
            policy.WithOrigins("http://example.com",
                "http://www.contoso.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
        ); ; 


});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//添加跨域请求
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();
#endregion
#elif Enable_Cors_with_endpoint_routing
#region
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
/*
 * CORS with default policy and middleware
 * 添加跨域请求
 */
builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(
        policy => 
        {
            policy.WithOrigins("http://example.com",
                "http://www.contoso.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
        );
});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
/*
 app.UseCors 启用 CORS 中间件. 由于默认的策略没有被配置,
 app.UseCors() 不能单独的启用CORS
*/
app.UseCors();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/echo",context=>context.Response.WriteAsync("echo"))
    .RequireCors(MyAllowSpecificOrigins);
    /*允许跨域请求*/
    endpoints.MapControllers().RequireCors(MyAllowSpecificOrigins);
    /*不允许跨域请求*/
    endpoints.MapRazorPages();
});
app.Run();
#endregion
#elif Enable_CORS_with_attributes
#region
/*映射到WidgetController*/
var builder =WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => 
{
    options.AddPolicy("Policy1", policy => 
    {
        policy.WithOrigins("http://example.com",
                                "http://www.contoso.com");
    });
    options.AddPolicy("AnotherPolicy", policy => 
    {
        policy.WithOrigins("http://www.contoso.com")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
builder.Services.AddControllers();

var app=builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
#elif Disable_CORS
#region
/*映射到ValuesController*/
var builder =WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => 
{
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.WithOrigins("http://example.com",
                                "http://www.contoso.com")
                    .WithMethods("PUT", "DELETE", "GET");
        });

});

builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app =builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
#endregion

#elif CORS_policy_options
#region 

using Microsoft.Net.Http.Headers;

var MyAllowSpecificOrigins = "_MyAllowSubdomainPolicy";

var builder = WebApplication.CreateBuilder(args);

/*场景一 设置允许访问的源 Set the allowed origins
builder.Services.AddCors(options => 
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
       policy =>
       {
           policy.WithOrigins("https://*.example.com")
                //设置通配符跨域
               .SetIsOriginAllowedToAllowWildcardSubdomains();
       });

});
*/
/*场景二 设置允许HTTP 方法 Set the allowed HTTP methods*/
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://example.com")
                  .AllowAnyMethod();
        });
});
/*场景三 设置允许请求Header Set the allowed request headers*/
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://example.com")
                 .WithHeaders(HeaderNames.ContentType, "x-custom-header")
                 .AllowAnyHeader();/*也可以允许任何Header*/
        });
});
/*场景四 暴露HTTP 响应头部 Set the exposed response headers*/
builder.Services.AddCors(options=>
{
    options.AddPolicy("MyExposeResponseHeadersPolicy",
      policy =>
      {
          policy.WithOrigins("https://*.example.com")
                 .WithExposedHeaders("x-custom-header");
      });
});
/*场景五 Credentials 在跨域请求中的应用 Credentials in cross-origin requests*/
builder.Services.AddCors(options => 
{
    options.AddPolicy("MyMyAllowCredentialsPolicy",
        policy =>
        {
            /*
             注意  客户端必须设置 XMLHttpRequest.withCredentials to true.
                  服务器端必须设置 AllowCredentials
             */
            policy.WithOrigins("http://example.com")
                   .AllowCredentials();
        });
});


builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
#endregion
#endif