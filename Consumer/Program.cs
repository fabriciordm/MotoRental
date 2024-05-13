

using MotoRental.CrossCutting.MessageBus.Rabbitmq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MotoRental.Consumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<MotorentalExchangeCreatedSubscriber>();

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

builder.Services.Configure<RabbitSettings>(rabbit => config.GetSection("RabbitSettings").Bind(rabbit));

var app = builder.Build();
app.Run();
