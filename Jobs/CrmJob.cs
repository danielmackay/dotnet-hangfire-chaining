namespace dotnet_hangfire.Jobs;

public class CrmJob
{
    public async Task Process()
    {
        Console.WriteLine("Updating CRM...");
        await Task.Delay(1000);
    }
}
