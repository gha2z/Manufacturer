namespace IntrManApp.Api;

public class SignalrService : BackgroundService
{
    public SignalrService(ILoggerFactory loggerFactory) => Logger = loggerFactory.CreateLogger<SignalrService>();

    public ILogger Logger { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("Intrepid SignalR service is starting.");

        stoppingToken.Register(() => Logger.LogInformation("Intrepid SignalR service is stopping."));

        while (!stoppingToken.IsCancellationRequested)
        {
            Logger.LogInformation("Intrepid SignalR service is working the background.");

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }

        Logger.LogInformation("Intrepid SignalR has stopped.");
    }

}
