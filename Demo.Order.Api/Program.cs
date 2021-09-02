using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Demo.NServiceBus;
using Demo.Order.ServiceContracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo.Order.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSendOnlyEndpoint<Program>(Routes)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        
        private static ImmutableDictionary<Assembly, string> Routes()
            => ImmutableDictionary.Create<Assembly, string>()
                .AddRange(new Dictionary<Assembly, string>
                {
                    {typeof(CreateOrder).Assembly, "Demo.Order.Endpoint"}
                });
    }
}