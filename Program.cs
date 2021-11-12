using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Worms_lab.services;
using Worms_lab.Strategies;

namespace Worms_lab
{
    class Program
    {
        public Position Position { get; private set; }
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)

                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<WorldSimulatorService>();
                    services.AddSingleton<WorldSimulator>();
                    services.AddSingleton<NameGenerator>();
                    services.AddSingleton<FoodGenerator>();
                    services.AddSingleton(sp => new WorldStateWriter(hostContext.Configuration.GetSection("WorldStateFileName").Value));
                    services.AddSingleton<IBehaviorStrategy, CleverMoveStrategy>();
                });
        }
    }




}
