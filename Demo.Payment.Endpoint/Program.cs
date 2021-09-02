using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Demo.NServiceBus;
using Demo.Payment.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.Payment.Endpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UsePublishAndSendEndpoint<Program>(Routes)
                .ConfigureServices((hostContext, services) => { services.AddHostedService<Worker>(); });

        private static ImmutableDictionary<Assembly, string> Routes()
            => ImmutableDictionary.Create<Assembly, string>()
                .AddRange(new Dictionary<Assembly, string>
                {
                    {typeof(MakePayment).Assembly, typeof(Program).Namespace}
                });
    }
}