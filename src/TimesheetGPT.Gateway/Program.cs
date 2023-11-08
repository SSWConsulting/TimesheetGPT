using TimesheetGPT.WebAPI;
using TimesheetGPT.WebUI;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets(typeof(Program).Assembly);

builder.Services.AddTimesheetGptUi(builder.Configuration);
builder.Services.AddTimesheetGptApi(builder.Configuration);

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.UseTimesheetGptApi();

app.UseTimesheetGptUi();

app.Run();