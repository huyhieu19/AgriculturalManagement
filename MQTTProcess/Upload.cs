namespace MQTTProcess
{
    public class Upload1
    {
        private CancellationTokenSource cts = new CancellationTokenSource();

        public void Start()
        {
            // Bắt đầu công việc ở đây.
            Task.Run(() => DoWork(cts.Token));
        }

        public void Stop()
        {
            // Yêu cầu hủy công việc.
            cts.Cancel();
            // Hoặc bạn có thể sử dụng cách sau để yêu cầu không hủy.
            // cts = new CancellationTokenSource();
        }

        private void DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Thực hiện công việc.
                // Nếu cancellationToken.IsCancellationRequested trở thành true, hãy dừng công việc.
                Console.WriteLine($"Thuc hien task");
                Task.Delay(TimeSpan.FromSeconds(3));
            }

        }
    }
}
