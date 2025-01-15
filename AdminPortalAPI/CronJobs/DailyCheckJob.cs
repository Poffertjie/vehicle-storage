using AdminPortalAPI.Services;
using Quartz;

namespace AdminPortalAPI.CronJobs;

public class DailyCheckJob : IJob
{
    private readonly CustomerVehicleService _customerVehicleService;

    public DailyCheckJob(CustomerVehicleService customerVehicleService)
    {
        _customerVehicleService = customerVehicleService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _customerVehicleService.CreateDailyCheckEntries();
    }
}