using System;
using System.Collections.Immutable;
using System.Reflection;
using Demo.NServiceBus.ServiceContracts;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using NServiceBus.Json;

namespace Demo.NServiceBus
{
    public static class NServiceBusExtensions
    {
        public static IHostBuilder UseSendOnlyEndpoint<T>(
            this IHostBuilder builder,
            Func<ImmutableDictionary<Assembly, string>> routes)
        {
            return builder.UseNServiceBus(ctx =>
            {
                var config = builder.GetConfig<T>(routes());
                config.SendOnly();
                return config;
            });
        }
        
        public static IHostBuilder UsePublishAndSendEndpoint<T>(
            this IHostBuilder builder,
            Func<ImmutableDictionary<Assembly, string>> routes)
        {
            builder.UseNServiceBus(ctx => builder.GetConfig<T>(routes()));

            return builder;
        }
        
        private static EndpointConfiguration GetConfig<T>(
            this IHostBuilder _,
            ImmutableDictionary<Assembly, string> routes)
        {
            var endpointConfig = new EndpointConfiguration(typeof(T).Namespace);
            endpointConfig.EnableInstallers();
            endpointConfig.UseSerialization<SystemJsonSerializer>();

            endpointConfig
                .SetConventions()
                .ConfigureTransport()
                .ConfigureRouting(routes);
                
            return endpointConfig;
        }

        private static EndpointConfiguration SetConventions(this EndpointConfiguration configuration)
        {
            var conventions = configuration.Conventions();
            conventions.DefiningMessagesAs(i =>  typeof(IAmCommand).IsAssignableFrom(i) || typeof(IAmEvent).IsAssignableFrom(i));
            conventions.DefiningCommandsAs(i => typeof(IAmCommand).IsAssignableFrom(i));
            conventions.DefiningEventsAs(i => typeof(IAmEvent).IsAssignableFrom(i));
            return configuration;
        }
        private static TransportExtensions<RabbitMQTransport> ConfigureTransport(this EndpointConfiguration configuration)
        {
            var transport = configuration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString("amqp://guest:guest@localhost:5672");
            return transport;
        }

        private static TransportExtensions<RabbitMQTransport> ConfigureRouting(
            this TransportExtensions<RabbitMQTransport> transport,
            ImmutableDictionary<Assembly, string> routes)
        {
            var routing = transport.Routing();
            foreach (var (key, value) in routes)
            {
                routing.RouteToEndpoint(key, value);
            }

            return transport;
        }
    }
}