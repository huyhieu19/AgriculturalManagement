using JobBackground;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class JobSchedulerHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private bool _hasStarted = false;

    public JobSchedulerHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Chạy ScheduleDapperJob lần đầu tiên khi ứng dụng bắt đầu chạy
        if (!_hasStarted)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var jobSchedulerService = scope.ServiceProvider.GetRequiredService<JobSchedulerDeviceDriver>();//dòng mã này nghĩa là bạn đang yêu cầu DI container cung cấp một thể hiện của lớp JobSchedulerDeviceDriver và lưu trữ nó trong biến jobSchedulerService.Sau khi dòng này được thực hiện, bạn có thể sử dụng jobSchedulerService để gọi các phương thức hoặc thuộc tính của JobSchedulerDeviceDriver.
                await jobSchedulerService.ScheduleJobs();// gọi đến hàm cần thực hiện
            }

            _hasStarted = true;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}