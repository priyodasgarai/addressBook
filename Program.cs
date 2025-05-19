using addressBook.DependencyInjection;
using addressBook.Middlewares;
using Microsoft.Extensions.FileProviders;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
//builder.Host.useserl


//Add Serilog for Loging---------------------------------------
Log.Logger = new LoggerConfiguration()
.Enrich.FromLogContext()
.WriteTo.Console()
.WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();

builder.Host.UseSerilog();
Log.Logger.Information("Application is building.....");
//---------------------Serilog-----------------------------------



builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationService(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/***********Add Cors*************
builder.Services.AddCors(
   builder =>
   {
       builder.AddDefaultPolicy(options =>
       {
           options.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowAnyOrigin()
          // .AllowAnyMethod()
           .AllowCredentials();
           // .WithOrigins("https://localhost:7025");
       });
   });
   ***********Add Cors*************/
try
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
        options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
        options.AddPolicy("StorePolicy", policy => policy.RequireRole("Store"));
    });
    var app = builder.Build();
    // app.UseCors();
    app.UseCors(policy =>
           policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
     );
    app.UseSerilogRequestLogging();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    /*app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
                Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
        RequestPath = "/Resources"
    });*/



    app.UseInfrastructructureService();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    Log.Logger.Information("Application is running.....");
    app.Run();

}
catch (Exception ex)
{
    Log.Logger.Error(ex, "Application failed to start......");
}
finally
{
    Log.CloseAndFlush();
}
