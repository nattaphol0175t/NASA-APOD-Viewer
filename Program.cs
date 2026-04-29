using NasaApodWeb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<NasaService>();

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();

app.Run();