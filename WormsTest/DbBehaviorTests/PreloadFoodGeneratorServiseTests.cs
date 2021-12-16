using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worms_lab.Data;
using Worms_lab.DBAware;
using Worms_lab.Simulator.Services;
using Worms_lab.Simulator;

namespace WormsTest.DbBehaviorTests
{
    [TestFixture]
    class PreloadFoodGeneratorServiseTests : DbBehaviourServicesTestBase
    {
        public PreloadFoodGeneratorServiseTests() : base(new DbContextOptionsBuilder<EnvironmentContext>()
                    .UseInMemoryDatabase(databaseName: "EnvironmentDatabase")
                    .Options)
        { }

        [SetUp]
        public void SetUp()
        {
            SeedData();
        }

        [Test] 
        public void AutoIncrementCheck()
        {
            var worldBehaviourService = new DbAwareWorldBehaviourService(ContextFactory);
            var foodGenerator = new PreloadFoodGeneratorService(worldBehaviourService, new BehaviorNameOption(behaviorName));
            var state = new WorldState().AsReadOnly();

            for (int i = 1; i <= 3; ++i) {
                var food = foodGenerator.Generate(state);
                Assert.AreEqual(worldBehaviourService.GetFoodOnStep(i), food);
            }           

        }

        [Test]
        public void FoodAlreadyExists()
        {
            var worldBehaviourService = new DbAwareWorldBehaviourService(ContextFactory);
            var foodGenerator = new PreloadFoodGeneratorService(worldBehaviourService, new BehaviorNameOption(behaviorName));
            var state = new WorldState();
            state.Food.Add(worldBehaviourService.GetFoodOnStep(1));
            try
            {
                foodGenerator.Generate(state.AsReadOnly());
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("Loaded food already exists", exc.Message);
            }
        }

        [Test]
        public void StepNotExists()
        {
            var worldBehaviourService = new DbAwareWorldBehaviourService(ContextFactory);
            var foodGenerator = new PreloadFoodGeneratorService(worldBehaviourService, new BehaviorNameOption(behaviorName));

            foodGenerator.setStepNum(-1);
            try
            {
                foodGenerator.Generate(new WorldState().AsReadOnly());
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual($"Step -1 does not exists.", exc.Message);
            }
        }



    }
}
