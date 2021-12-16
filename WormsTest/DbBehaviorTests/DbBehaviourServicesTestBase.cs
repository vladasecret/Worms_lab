using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Worms_lab.Data;
using Worms_lab.DBAware;
using System.Linq;

namespace WormsTest.DbBehaviorTests
{
    class DbBehaviourServicesTestBase
    {
        public DbBehaviourServicesTestBase(DbContextOptions<EnvironmentContext> options)
        {
            ContextFactory = new TestDbContextFactory(options);
            behaviorName = "TestBehavior";
        }

        protected IDbContextFactory<EnvironmentContext> ContextFactory { get; set; }

        protected string behaviorName { get; set; }

        protected void SeedData()
        {
            using var context = ContextFactory.CreateDbContext();
            Behavior behavior = new() { BehaviorSteps = new(), Name = behaviorName };

            for (int i = 1; i < 5; ++i)
            {
                behavior.BehaviorSteps.Add(new BehaviorStep() { StepNum = i, X = i, Y = i });
            }
            behavior.BehaviorSteps.Add(new BehaviorStep() { StepNum = 5, X = 1, Y = 1 });
            context.Add(behavior);
            context.SaveChanges();
        }

        public void TestLoading()
        {
            var worldBehaviourService = new DbAwareWorldBehaviourService(ContextFactory);
            worldBehaviourService.Load(behaviorName);
            var step = 1;
            var food = worldBehaviourService.GetFoodOnStep(step);
            Assert.IsNotNull(food, $"Food should appear on step {step}");
        }

    }

}
