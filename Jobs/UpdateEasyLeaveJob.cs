namespace dotnet_hangfire.Jobs;

public class UpdateEasyLeaveJob
{
    public async Task Process()
    {
        Console.WriteLine("Updating EasyLeave...");
        await Task.Delay(1000);
    }
}
