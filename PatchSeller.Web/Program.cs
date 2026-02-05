using Blazored.LocalStorage;
using MudBlazor;
using MudBlazor.Services;
using PatchSeller.Web.Components;
using PatchSeller.Web.Services;
using PatchSeller.Web.Services.Admin;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddScoped<AccessService>();
builder.Services.AddScoped<DrawerService>();
builder.Services.AddScoped<AdminCategoryService>();
builder.Services.AddScoped<AdminPlatformService>();
builder.Services.AddScoped<AdminPublisherService>();
builder.Services.AddScoped<AdminStaffService>();
builder.Services.AddScoped<AdminUserService>();
builder.Services.AddScoped<AdminDiscountService>();
builder.Services.AddScoped<AdminRankService>();

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7226/")
});

builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/error/{0}");
app.UseHttpsRedirection();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
