using BlogWebApi.DependencyInjections.Configuration;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injections
builder.Services
    .AddInfrastructure()
    .AddPresentation()
    .AddServices()
    .AddAuth(builder.Configuration)
    .AddSerilog()
        .AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
        })
        .AddHttpClient()
        .AddHttpContextAccessor();


#region serilog
var config = new ConfigurationBuilder().
    AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.MongoDBBson("mongodb://localhost:27017/serilogdb")
    .CreateLogger();

builder.Host.UseSerilog();
//try
//{
//    Log.Information("Application starting here");
//}
//catch (Exception ex)
//{
//    Log.Fatal(ex, "Failed to start");
//}
//finally
//{
//    Log.CloseAndFlush();
//}

//builder.Host.UseSerilog((context, config) =>
//{
//    config.ReadFrom.Configuration(context.Configuration);
//    var nk = context.Configuration;
//    Console.WriteLine(nk);
//});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();//Serilog

app.UseCors();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
