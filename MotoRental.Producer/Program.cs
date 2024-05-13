

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MotoRental.Producer.Producers;
using MotoRental.Producer;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IRabbitMQClient, MotoRental.Producer.RabbitMQClient>();

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

builder.Services.Configure<MotoRental.CrossCutting.MessageBus.Rabbitmq.RabbitSettings>(rabbit => config.GetSection("RabbitSettings").Bind(rabbit));

var app = builder.Build();

var port = app.Configuration.GetValue<int>("Kestrel:Endpoints:Http:UrlPort", 5184);

app.Run(async context =>
{
    await context.Response.WriteAsync("CONSUMER");
});



app.Run();


