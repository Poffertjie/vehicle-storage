using System.Text;
using AdminPortalAPI.Extensions;
using AdminPortalAPI.Identity;
using AdminPortalAPI.Services;
using AutoWrapper;
using Database.DbContext;
using Database.DbContext.DbModels;
using Database.DbContext.IdentityModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AdminPortalAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpContextAccessor();
        builder.Services.SetupConfig(builder.Configuration);
        builder.Services.SetupScheduler();

        builder.Services.AddScoped<AuthenticationService>();
        builder.Services.AddScoped<CustomerService>();
        builder.Services.AddScoped<VehicleService>();
        builder.Services.AddScoped<ServiceAdvisorService>();
        builder.Services.AddScoped<CustomerVehicleService>();
        builder.Services.AddScoped<MediaService>();
        builder.Services.AddScoped<LookupService>();
        builder.Services.AddScoped<StorageService>();
        builder.Services.AddScoped<CompanyService>();
        
        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        }).AddDefaultTokenProviders();

        // Identity Services
        builder.Services.AddScoped<IUserStore<ApplicationUser>, ApplicationUserStore>();
        builder.Services.AddScoped<IRoleStore<ApplicationRole>, ApplicationRoleStore>();

        builder.Services.AddDbContext<VehicleManagementContext>(options =>
            options
                .UseLazyLoadingProxies(true)
                .UseSqlServer(Config.DB));

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Config.JwtKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
        {
            ShowIsErrorFlagForSuccessfulResponse = true,
            ShowStatusCode = true,
            ShowIsValidationErrorFlagForSuccessfulResponse = true,
            IsApiOnly = true,
            IsDebug = true
        });



        app.SeedData();


        app.Run();
    }
}