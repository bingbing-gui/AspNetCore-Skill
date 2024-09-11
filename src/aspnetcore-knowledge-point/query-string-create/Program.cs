var app = WebApplication.Create();

app.Run(async context =>
{
    var dicts = new Dictionary<string, string>()
    {
        ["id"] = "001",
        ["name"] = "gui bingbing",
        ["birthday"] = "1986/08/30",
        ["guid"] = Guid.NewGuid().ToString(),
        ["artist"] = "Bill",
        ["formula"] = "10 * 5 = 50"
    };

    var queryString = QueryString.Create(dicts);

    context.Response.Headers.Append("Content-Type", "text/html");
    await context.Response.WriteAsync($@"<html>
    <head>
        <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.5/css/bulma.css"" />
    </head>
    <body class=""content"">
    <div class=""container"">
    <h1>使用 QueryString.Create 创建编码的URL查询字符串</h1>
    <strong>Input</strong>
    ");
    await context.Response.WriteAsync("<ul>");
    foreach (var k in dicts)
    {
        await context.Response.WriteAsync($"<li>{k.Key} = {k.Value}</li>");
    }
    await context.Response.WriteAsync("</ul>");
    await context.Response.WriteAsync("<strong>Output</strong><br/>");
    await context.Response.WriteAsync(queryString.Value);
    await context.Response.WriteAsync("</ul>");
    await context.Response.WriteAsync(@"</div></body></html>");
});

app.Run();