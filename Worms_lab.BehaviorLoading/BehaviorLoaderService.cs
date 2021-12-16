using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Worms_lab.Data;

namespace Worms_lab.BehaviorLoading
{
    public class BehaviorLoaderService
    {
        private readonly IDbContextFactory<EnvironmentContext> contextFactory;
        public BehaviorLoaderService (IDbContextFactory<EnvironmentContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public bool Exists(string behaviorName)
        {
            using var context = contextFactory.CreateDbContext();
            
            var behavior = context.Behaviors.AsNoTracking()
                .Where(b => b.Name.Equals(behaviorName))
                .FirstOrDefault();
            if (behavior == null)
                return false;
            return true;
            
        }
        public void Load(string behaviorName, List<(int x, int y)> positions)
        {
            using var context = contextFactory.CreateDbContext();
            var behavior = new Behavior() { Name = behaviorName, BehaviorSteps = new() };
            int i = 1;
            foreach (var pair in positions)
            {
                behavior.BehaviorSteps.Add(new BehaviorStep() { StepNum = i, X = pair.x, Y = pair.y });
                ++i;
            }
            context.Add(behavior);
            context.SaveChanges();
        }
    }
}
