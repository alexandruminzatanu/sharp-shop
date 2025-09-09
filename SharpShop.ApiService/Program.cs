using SharpShop.Data;
using SharpShop.Business;
using SharpShop.Models;
using Microsoft.EntityFrameworkCore;
using SharpShop.Data.SQLContext;

var builder = WebApplication.CreateBuilder(args);
var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

builder.AddServiceDefaults(); 
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddRepositoryDependencies(builder.Configuration);
builder.Services.AddServiceDependencies(); 
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
// if (mongoDBSettings == null)
// {
//     throw new InvalidOperationException("MongoDBSettings configuration section is missing or invalid.");
// }
// builder.Services.AddDbContext<SharpShopMongoContext>(options =>
//     options.UseMongoDB(mongoDBSettings.AtlasURI ?? "", mongoDBSettings.DatabaseName ?? ""));


var app = builder.Build();
app.UseExceptionHandler();
app.MapControllers();
app.MapDefaultEndpoints();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SharpShop API V1");
        c.RoutePrefix = string.Empty;
    });
}
app.Run();
