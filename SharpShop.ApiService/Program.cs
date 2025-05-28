using SharpShop.Data;
using SharpShop.Business;

var builder = WebApplication.CreateBuilder(args);  
builder.AddServiceDefaults(); 
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddRepositoryDependencies(builder.Configuration);
builder.Services.AddServiceDependencies(); 
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
