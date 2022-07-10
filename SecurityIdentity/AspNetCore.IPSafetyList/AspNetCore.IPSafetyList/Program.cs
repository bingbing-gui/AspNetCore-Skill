using AspNetCore.IPSafetyList.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var configurationManager=builder.Configuration;
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

app.UseAuthorization();

app.UseMiddleware<AdminSafetyListMiddlewares>(configurationManager.GetSection("AdminSafetyList").Value);

app.MapControllers();

app.Run();
