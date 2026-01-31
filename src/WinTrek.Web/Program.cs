using WinTrek.Core.Services;
using WinTrek.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register game services
builder.Services.AddScoped<IGameEngine, GameEngine>();
builder.Services.AddScoped<GameService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UsePathBase("/wintrek");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<WinTrek.Web.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
