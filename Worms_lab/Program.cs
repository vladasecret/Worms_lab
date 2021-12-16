using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Worms_lab.Simulator;
using Worms_lab.Simulator.Services;
using Worms_lab.Simulator.Strategies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Worms_lab.Data;
using Worms_lab.DBAware;

namespace Worms_lab
{
    class Program
    {
        
        static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.ArgumentException exc)
            {
                System.Console.WriteLine(exc.Message);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)

                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<WorldSimulatorService>();
                    
                    services.AddSingleton<WorldSimulator>();
                    services.AddSingleton<NameGeneratorService>();
                    
                    services.AddSingleton(sp => new WorldStateWriterService(hostContext.Configuration.GetSection("WorldStateFileName").Value));
                    services.AddSingleton<IBehaviorStrategy, CleverMoveStrategy>();
                    
                    services.AddSingleton<IFoodGeneratorService, PreloadFoodGeneratorService>();
                    services.AddSingleton(provider => new BehaviorNameOption(args[0]));
                    services.AddSingleton<IWorldBehaviorService, DbAwareWorldBehaviourService>();

                    services.AddDbContextFactory<EnvironmentContext>(options => options.UseSqlServer(
                            hostContext.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Worms_lab")));
                });
#if RELEASE

            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            });
#endif
            return builder;
        }
    }




}
