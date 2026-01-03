using CoreCompetencyInterviewGenerator.Components;
using CoreCompetencyInterviewGenerator.Data;
using CoreCompetencyInterviewGenerator.ViewModels;

var builder = WebApplication.CreateBuilder(args);
string dbPath = builder.Configuration["DatabaseSettings:DatabaseFilePath"] ?? "Not Set";

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<AppDbContextFactory>();

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
