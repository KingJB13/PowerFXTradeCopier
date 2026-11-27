using PowerFXTradeCopier.API;
using PowerFXTradeCopier.Infrastructure.Notifier.Hubs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Initialize Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAPIDependencyInjection(builder.Configuration);

builder.Services.AddSignalR();

try
{
    Log.Information("Starting PowerFXTradeCopier API");

    var app = builder.Build();

    // Signal routes
    app.MapHub<SignalHub>("/hubs/signals");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "PowerFXTradeCopier API terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
