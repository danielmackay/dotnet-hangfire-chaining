namespace dotnet_hangfire.Jobs;

public class TimeProJob
{
    public async Task Process()
    {
        //var jobId = context.BackgroundJob.Id;
        if (DateTime.UtcNow.Ticks % 2 == 0)
        {
            Console.WriteLine("Failed to update CRM.");
            throw new ApplicationException("Failed to update CRM.");
        }

        Console.WriteLine("Updating TimePro...");
        await Task.Delay(1000);
    }
}
