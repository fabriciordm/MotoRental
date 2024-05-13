using MotorCycle.Data.Context;
using MotorCycle.Data.Repositories;
using Motorcycle.Domain.Handlers;
using Motorcycle.Domain.Interfaces.Commons;
using Motorcycle.Domain.Interfaces.Repositories;
using MotoRental.Services.AppServices;
using MotoRental.Services.Interfaces;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Motorcycle.Domain.Core.Notifications;
using Motorcycle.Domain.CommandHandlers;
using Motorcycle.Domain.Interfaces;
using Motorcycle.Domain.Commands.Motorcycle;
using MotoRental.Data.UoW;
using Motorcycle.Domain.Commands.DeliveryDriver;
using Motorcycle.Domain.Commands.Rental;
using MotoRental.CrossCutting.MessageBus.Rabbitmq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
builder.Services.AddScoped<IMotorcycleService, MotorcycleService>();
builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IRabbitMQClient, RabbitMQClient>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDeliveyDriverService, DeliveyDriverService>();
builder.Services.AddScoped<IRentalService, RentalService>();


var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();


builder.Services.Configure<RabbitSettings>(rabbit => config.GetSection("RabbitSettings").Bind(rabbit));


var connectionString = config.GetConnectionString("WebApiDatabase");

builder.Services.AddDbContext<MotoContext>(options =>
{
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("MotoRental.API"));
  
});



builder.Services.AddDbContext<MotoContext>(options =>
{
    options.UseNpgsql("ConnectionStrings");   
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
builder.Services.AddScoped<INotificationHandler<CreateMotorcycleCommand>, MotorcycleCommandHandler>();
builder.Services.AddScoped<INotificationHandler<UpdateMotorcycleCommand>, MotorcycleCommandHandler>();
builder.Services.AddScoped<INotificationHandler<DeleteMotorcycleCommands>, MotorcycleCommandHandler>();

builder.Services.AddScoped<INotificationHandler<CreateDeliveryDriverCommand>,DeliveryDriverCommandHandler> ();
builder.Services.AddScoped<INotificationHandler<CreateRentalCommand>, RentalCommandHandler>();
builder.Services.AddScoped<INotificationHandler<DeleteRentalCommand>, RentalCommandHandler>();
builder.Services.AddScoped<INotificationHandler<UpdateRentalCommand>, RentalCommandHandler>();





builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "MotoRental",
        Version = "v1"
    });
});

builder.Services.AddMvcCore()
    .AddApiExplorer();

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();


app.UseSwagger();

 app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }

    await next();
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nome da sua API V1");
   
});


app.MapGet("/", () => "MotoRental");
app.MapControllers();

app.Run();
