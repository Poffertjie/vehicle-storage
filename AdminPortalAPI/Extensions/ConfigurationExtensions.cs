using AdminPortalAPI.CronJobs;
using Database.DbContext;
using Database.DbContext.DbModels;
using Database.DbContext.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Shared.Enums;
using Shared.Helpers;

namespace AdminPortalAPI.Extensions;

public static class ConfigurationExtensions
{
    public static void SetupConfig(this IServiceCollection services, IConfiguration configuration)
    {
        Config.RawConfig rawConfig = new Config.RawConfig()
        {
            DB = configuration.GetConnectionString(nameof(rawConfig.DB)),
            JwtKey = configuration.GetSection(nameof(rawConfig.JwtKey)).Value,
        };
        Config.SetRawConfigInstance(rawConfig);
    }

    public static void SeedData(this WebApplication app)
    {
        //seed admin
        Task.Run(async () =>
        {
            using IServiceScope scope = app.Services.CreateScope();
            var dbContext =
                scope.ServiceProvider.GetRequiredService<VehicleManagementContext>();

            var userManager =
                scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            
            var userId = Guid.Empty;
            if (await dbContext.Users.AnyAsync(x => x.Email == "admin@topaz.com"))
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == "admin@topaz.com");

                if (user is not null)
                    userId = Guid.Parse(user.Id);
            }
            else
            {
                var result = await userManager.CreateAsync(new ApplicationUser()
                {
                    FullName = "admin",
                    Email = "admin@topaz.com",
                    SystemAdmin = true,
                }, "Admin@123");

                if (result.Succeeded)
                {
                    var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == "admin@topaz.com");

                    if (user is not null)
                        userId = Guid.Parse(user.Id);
                }
            }

            if (await dbContext.Companies.AnyAsync(x => x.Name == "Topaz") == false)
            {
                var company = await dbContext.Companies.AddAsync(new Company
                {
                    MODIFIED_BY = userId,
                    Name = "Topaz",
                    Address = "sarel baard straat",
                    ContactNumber = "054 289 1234"
                });

                await dbContext.SaveChangesAsync();

                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == "admin@topaz.com");

                if (user is not null)
                {
                    user.CompanyId = company.Entity.Id;
                    await dbContext.SaveChangesAsync();
                }
            }
            
            
            //seed roles
            foreach (var role in EnumHelper.GetValues<RolesEnum>())
            {
                var roleManager =
                    scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                if (await roleManager.RoleExistsAsync(role.ToString()) == false)
                {
                    await roleManager.CreateAsync(new ApplicationRole()
                    {
                        Name = role.ToString()
                    });
                }
            }
            
            //seed roles
            foreach (var value in EnumHelper.GetValues<StorageBayStatusEnum>())
            {
                if (await dbContext.StorageBayStatuses.AnyAsync(x => x.Id == (int)value) == false)
                {
                    await dbContext.StorageBayStatuses.AddAsync(new StorageBayStatus()
                    {
                        
                    });
                }
            }
            
            //seed roles
            foreach (var role in EnumHelper.GetValues<RolesEnum>())
            {
                var roleManager =
                    scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                if (await roleManager.RoleExistsAsync(role.ToString()) == false)
                {
                    await roleManager.CreateAsync(new ApplicationRole()
                    {
                        Name = role.ToString()
                    });
                }
            }
            
            if (await dbContext.Users.AnyAsync(x => x.Email == "serviceaccount@topaz.com") == false)
            {
                var result = await userManager.CreateAsync(new ApplicationUser()
                {
                    FullName = "Service Account",
                    Email = "serviceaccount@topaz.com",
                    SystemAdmin = true,
                }, "Admin@123");

                if (result.Succeeded)
                {
                    var serviceAccount = await userManager.FindByEmailAsync("serviceaccount@topaz.com");
                    await userManager.AddToRoleAsync(serviceAccount, RolesEnum.Serviceaccount.ToString());
                }
            }
            
        });
    }
    
    public static void SetupScheduler(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            var dailyJob = new JobKey(nameof(DailyCheckJob));
            q.AddJob<DailyCheckJob>(opts => opts.WithIdentity(dailyJob));
            q.AddTrigger(opts => opts
                    .ForJob(dailyJob)
                    .WithIdentity(nameof(DailyCheckJob))
                .WithDailyTimeIntervalSchedule(s =>
                {
                    s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(06, 00))
                    .Build();
                })
            );
            
            var weeklyJob = new JobKey(nameof(WeeklyCheckJob));
            q.AddJob<WeeklyCheckJob>(opts => opts.WithIdentity(weeklyJob));
            q.AddTrigger(opts => opts
                .ForJob(weeklyJob)
                .WithIdentity(nameof(WeeklyCheckJob))
                .WithCronSchedule("0 0 6 ? * MON *") 
            );
        });

        services.AddQuartzServer(options =>
        {
            options.WaitForJobsToComplete = true;
        });
    }
}