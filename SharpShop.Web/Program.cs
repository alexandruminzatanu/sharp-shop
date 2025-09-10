using System.IO;
using Microsoft.Extensions.FileProviders;

// Minimal host that serves an Angular SPA instead of a Blazor application.
// The Angular project is scaffolded under ClientApp. In development you run `npm start` there.
// In production you build the Angular app (output to ClientApp/dist) and the ASP.NET Core app
// serves the static files and falls back to index.html for client-side routing.

var builder = WebApplication.CreateBuilder(args);

// Service defaults (Aspire integration etc.)
builder.AddServiceDefaults();

// CORS so the Angular dev server (http://localhost:4200) can call downstream APIs directly.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularDev", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// (Optional) Add backend proxy endpoints or typed clients here if needed.

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("AngularDev");
app.UseStaticFiles(); // Serves wwwroot (you can copy Angular build here or serve from ClientApp/dist below)

// Serve Angular dist if it exists (production build scenario)
var angularDist = Path.Combine(app.Environment.ContentRootPath, "ClientApp", "dist");
if (Directory.Exists(angularDist))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(angularDist)
    });

    // Fallback for client-side routes -> index.html inside dist
    app.MapFallback(async context =>
    {
        var indexFile = Path.Combine(angularDist, "index.html");
        if (File.Exists(indexFile))
        {
            context.Response.ContentType = "text/html";
            await context.Response.SendFileAsync(indexFile);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
    });
}

// Simple health endpoint
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapDefaultEndpoints();

app.Run();
