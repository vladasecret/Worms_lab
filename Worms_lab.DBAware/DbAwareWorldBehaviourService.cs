using System;
using Worms_lab.Simulator;
using Worms_lab.Simulator.Services;
using Worms_lab.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Worms_lab.DBAware
{
    public class DbAwareWorldBehaviourService : IWorldBehaviorService
    {
        private readonly IDbContextFactory<EnvironmentContext> contextFactory;
        private List<BehaviorStep> steps;
        public DbAwareWorldBehaviourService(IDbContextFactory<EnvironmentContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        public Food GetFoodOnStep(int stepNumber)
        {
            var food = steps.Where(bs => bs.StepNum == stepNumber)
                .Select(bs => new Food(new Position(bs.X, bs.Y)))
                .FirstOrDefault();
            if (food == null)
                throw new ArgumentException($"step {stepNumber} does not exist");
            return food;
        }

        public void Load(string behaviorName)
        {
            using var context = contextFactory.CreateDbContext();
            var behavior = context.Behaviors.AsNoTracking()
                .Where(behavior => behavior.Name.Equals(behaviorName))
                .Include(b => b.BehaviorSteps).FirstOrDefault();
            if (behavior == null)
                throw new ArgumentException($"Behavior with name \"{behaviorName}\" does not exist");
            steps = behavior.BehaviorSteps;
        }
    }
}
