using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Demo.NServiceBus;
using Demo.Order.ApplicationServices.DomainEventHandlers;
using Demo.Order.ApplicationServices.MessageHandlers;
using Demo.Order.Domain;
using Demo.Order.Domain.Events;
using Demo.Order.Domain.Infrastructure;
using Demo.Order.Domain.Repositories;
using Demo.Order.Persistence;
using Demo.Order.ServiceContracts;
using Demo.Payment.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.Order.Endpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();
            DomainEvents.Services =  host.Services; 
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UsePublishAndSendEndpoint<Program>(Routes)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    AddDbContext(services);
                });

        private static IServiceCollection AddDbContext(IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IHandleDomainEvent<OrderSubmitted>, OrderSubmittedHandler>();
            
            services.AddDbContext<DemoContext>((sp, options) =>
            {
                options.EnableSensitiveDataLogging();
                options.UseNpgsql(
                    "Host=localhost;port=5432;Database=demo_event_driven;Username=postgres;Password=postgres",
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(DemoContext).Assembly.GetName().Name);
                    })
                    .UseSnakeCaseNamingConvention();
            });

            return services;
        }
        
        private static ImmutableDictionary<Assembly, string> Routes()
            => ImmutableDictionary.Create<Assembly, string>()
                .AddRange(new Dictionary<Assembly, string>
                {
                    {typeof(CreateOrder).Assembly, typeof(Program).Namespace},
                    {typeof(PaymentSucceeded).Assembly, "Demo.Payment.Endpoint"}
                });
    }
}