namespace IntrManApp.Api;

public class BackendService(ILoggerFactory loggerFactory) : BackgroundService
{
    public ILogger Logger { get; } = loggerFactory.CreateLogger<BackendService>();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            Logger.LogInformation("Intrepid backend service is starting.");
            stoppingToken.Register(() => Logger.LogInformation("Intrepid backend service is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                Logger.LogInformation("Intrepid backend service is working the background.");

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            Logger.LogInformation("Intrepid backend service has stopped.");
        }
        catch (OperationCanceledException ex)
        {
            Logger.LogInformation($"Intrepid backend service has cancelled: {ex.Message}");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "{Message}", ex.Message);

            // Terminates this process and returns an exit code to the operating system.
            // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
            // performs one of two scenarios:
            // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
            // 2. When set to "StopHost": will cleanly stop the host, and log errors.
            //
            // In order for the Windows Service Management system to leverage configured
            // recovery options, we need to terminate the process with a non-zero exit code.
            Environment.Exit(1);
        }
    }
   
}
