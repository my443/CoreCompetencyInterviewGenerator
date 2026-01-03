using CoreCompetencyInterviewGenerator.Components;
using CoreCompetencyInterviewGenerator.Data;
using CoreCompetencyInterviewGenerator.Helpers;
using CoreCompetencyInterviewGenerator.ViewModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var path = builder.Configuration["WordFileSettings:Location"];
AppSettings.WordTemplatePath = path;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite(connectionString)); // Using SQLite for the database      

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<AppDbContextFactory>();

builder.Services.AddScoped<CategoryViewModel>();
builder.Services.AddScoped<QuestionViewModel>();
builder.Services.AddScoped<InterviewViewModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
