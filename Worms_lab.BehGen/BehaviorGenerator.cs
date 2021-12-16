using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Worms_lab.BehaviorLoading;
using Worms_lab.Data;

namespace Worms_lab.BehGen
{
    public class BehaviorGenerator : IHostedService
    {
        private readonly IHostApplicationLifetime appLifetime;   
        private readonly string behaviorName;
        private readonly BehaviorLoaderService loader;
        private readonly PositionGeneratorService positionGeneratorService;


        public BehaviorGenerator(IHostApplicationLifetime appLifetime, BehaviorNameOption behaviorNameOption, BehaviorLoaderService loader, PositionGeneratorService positionGeneratorService)
        {
            behaviorName = behaviorNameOption.Name;
            this.appLifetime = appLifetime;
            this.loader = loader;
            this.positionGeneratorService = positionGeneratorService;
        }

        public bool Generate()
        {
            if (loader.Exists(behaviorName))
            {
                Console.WriteLine($"Behavior with name {behaviorName} already exists.");
                return false;
            }
            loader.Load(behaviorName, GenerateUniquePositions());
            return true;
            
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                Generate();
                appLifetime.StopApplication();
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private List<(int x, int y)> GenerateUniquePositions()
        {
            List<(int x, int y)> positionsList = new();
            PositionGeneratorService generator = new(0, 5);

            (int x, int y) position;
            for (int i = 0; i < 100; ++i)
            {
                do
                {
                    position = generator.Generate();
                }
                while (positionsList.Contains(position));
                positionsList.Add(position);
            }
            return positionsList;

        }
    }


}
