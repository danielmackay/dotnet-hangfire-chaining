using dotnet_hangfire.Jobs;
using Hangfire;
using Hangfire.Server;
using Hangfire.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("HangfireConnection");

// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseRouting();
// app.UseAuthorization();

app.UseHangfireDashboard();
//app.MapHangfireDashboard();

//Batch is a group of background jobs that is created atomically and considered as a single entity. Two jobs can be run as below.
// var batchId = BatchJob.StartNew(x =>
// {
//     x.Enqueue(() => Console.WriteLine("Job 1"));
//     x.Enqueue(() => Console.WriteLine("Job 2"));
// });

// JobStorage.Current.GetConnection().SetJobParameter(batchId, "key", "value");


var crmJobId = BackgroundJob.Enqueue<CrmJob>(job => job.Process());
var timeProJobId = BackgroundJob.ContinueJobWith<TimeProJob>(crmJobId, job => job.Process());
var updateEasyLeaveJobId = BackgroundJob.ContinueJobWith<UpdateEasyLeaveJob>(timeProJobId, job => job.Process());

app.Run();

// var jobClient = app.Services.GetRequiredService<IBackgroundJobClient>();
// jobClient.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
