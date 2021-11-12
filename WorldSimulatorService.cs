using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Worms_lab.services;
using Worms_lab.Strategies;

namespace Worms_lab
{
    public class WorldSimulatorService : IHostedService
    {

        private readonly WorldSimulator simulator;
        private readonly IBehaviorStrategy strategy;
        private readonly NameGenerator nameGenerator;
        private readonly IHostApplicationLifetime appLifetime;
        private int stepNum; 

        public WorldSimulatorService(WorldSimulator simulator, IBehaviorStrategy strategy, NameGenerator nameGenerator, 
            IHostApplicationLifetime appLifetime)
        {
            this.strategy = strategy;
            this.simulator = simulator;
            this.nameGenerator = nameGenerator;
            this.appLifetime = appLifetime;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                WorldState state = new();
                state.Worms.Add(new Worm(state.AsReadOnly(), strategy, nameGenerator.GenerateName()));
                simulator.InitState(state);
                for (stepNum = 0; stepNum < 100; ++stepNum)
                {
                    simulator.MakeStep();
                }
                appLifetime.StopApplication();
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            stepNum = 100;
            return Task.CompletedTask;
        }
    }
}
