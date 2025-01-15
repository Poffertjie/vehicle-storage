using AdminPortal.Components;
using AdminPortal.Extensions;
using AdminPortal.Handlers;
using AdminPortal.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

namespace AdminPortal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.SetupConfig(builder.Configuration);
        
        // Add services to the container.
        builder.Services.AddRazorComponents(options =>
            {
                options.DetailedErrors = true;
            })
            .AddInteractiveServerComponents(options =>
            {
                options.DetailedErrors = true;
            });
        
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddAuthorization();
        builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();
        builder.Services.AddCascadingAuthenticationState();

        builder.Services.AddMudServices();
        
        builder.Services.AddHttpClient(nameof(Config.API), c =>
        {
            c.BaseAddress = new System.Uri(Config.API);
            c.Timeout = TimeSpan.FromMinutes(10);
        });
        
        builder.Services.AddScoped<HttpService>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}