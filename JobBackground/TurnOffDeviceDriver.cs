using Quartz;

namespace JobBackground
{
    internal class TurnOffDeviceDriver : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
