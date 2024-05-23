using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Reflection;
using Serilog.Exceptions;
using CSF.Repository.SqlServer;
using CSF.Entities;
using CSF.Repository.SqlServer.Implementation;
using CSF.Services.Interfaces;
using CSF.Services.Implementation;
using FluentValidation.AspNetCore;
using FluentValidation;
using MassTransit;
using CSF.Services.ValidationConfig;
using CSF.ViewModel;
using CSF.Domain;
using AgendaApi.AsyncMessaging.RabbitMQService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureLogging();
builder.Host.UseSerilog();
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        //busFactoryConfigurator.Host("rabbitmq", hostConfigurator => { });
        busFactoryConfigurator.Host("rabbitmq", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

builder.Services.AddScoped<IValidator<AddAppointmentDto>, AppointmentValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CSF.Repository.SqlServer.AppContext>();

builder.Services.AddTransient<IAppContext, CSF.Repository.SqlServer.AppContext>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Repositories
builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();

// Services
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IAppointmentService, AppointmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var appContext = scope.ServiceProvider.GetRequiredService<CSF.Repository.SqlServer.AppContext>();
        appContext.Database.EnsureCreated();
        appContext.Seed();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    string test = "";
    test.Split("");
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}
