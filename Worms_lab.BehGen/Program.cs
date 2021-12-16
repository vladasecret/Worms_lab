using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Worms_lab.BehaviorLoading;
using Worms_lab.Data;

namespace Worms_lab.BehGen
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length != 1)
                throw new ArgumentException("You need to enter behavior name  in the arguments");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            var builder = Host.CreateDefaultBuilder(args)

                .ConfigureServices((hostContext, services) =>
                {
                
                    services.AddHostedService<BehaviorGenerator>();
                    services.AddSingleton(provider => new BehaviorNameOption(args[0]));
                    services.AddSingleton(provider => new PositionGeneratorService(0, 5));
                    services.AddSingleton<BehaviorLoaderService>();
                    services.AddDbContextFactory<EnvironmentContext>(options =>
                    {
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Worms_lab"));
                    });
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
