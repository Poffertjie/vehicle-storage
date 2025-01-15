using AdminPortalAPI.Services;
using Quartz;

namespace AdminPortalAPI.CronJobs;

public class WeeklyCheckJob : IJob
{
    private readonly CustomerVehicleService _customerVehicleService;

    public WeeklyCheckJob(CustomerVehicleService customerVehicleService)
    {
        _customerVehicleService = customerVehicleService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _customerVehicleService.CreateWeeklyCheckEntries();
    }
}