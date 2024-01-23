using Rollbar.PlugIns.Serilog;
using Rollbar;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//******************************************************************************************************************
RollbarInfrastructureConfig rollbarInfrastructureConfig =
    new RollbarInfrastructureConfig(
        "XX",
        "production"
        );


var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console() 
    .WriteTo.DatadogLogs("YY")
    .WriteTo.RollbarSink(rollbarInfrastructureConfig, TimeSpan.FromSeconds(3), LogEventLevel.Debug)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
//******************************************************************************************************************


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


