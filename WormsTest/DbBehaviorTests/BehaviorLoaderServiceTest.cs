using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worms_lab.BehaviorLoading;
using Worms_lab.Data;

namespace WormsTest.DbBehaviorTests
{
    [TestFixture]
    class BehaviorLoaderServiceTest
    {
        private IDbContextFactory<EnvironmentContext> contextFactory;
        public BehaviorLoaderServiceTest()
        {
            contextFactory = new TestDbContextFactory(new DbContextOptionsBuilder<EnvironmentContext>()
                .UseInMemoryDatabase(databaseName: "EnvironmentDatabase")
                .Options);
        }


        [Test]
        [TestCase("TestBehavior")]
        public void AddBehavior(string behaviorName)
        {
            List<(int x, int y)> coords = new();

            var loader = new BehaviorLoaderService(contextFactory);
            loader.Load(behaviorName, coords);

            using var context = contextFactory.CreateDbContext();
            var names = context.Behaviors.AsNoTracking().Select(b => b.Name).ToList();

            Assert.AreEqual(1, names.Count);
            Assert.AreEqual(behaviorName, names[0]);
        }


        [Test]
        [TestCase("TestBehavior")]
        public void ExistsCheck(string behaviorName)
        {
            List<(int x, int y)> coords = new();

            var loader = new BehaviorLoaderService(contextFactory);
            loader.Load(behaviorName, coords);

            Assert.AreEqual(true, loader.Exists(behaviorName));
            Assert.AreEqual(false, loader.Exists($"{behaviorName}2"));
        }


        [Test]
        [TestCase(5, "test")]
        public void CheckStepNums(int stepNums, string behaviorName)
        {
            List<(int x, int y)> coords = new();
            for (int i = 0; i < stepNums; ++i)
            {
                coords.Add((i, i));
            }

            var loader = new BehaviorLoaderService(contextFactory);
            loader.Load(behaviorName, coords);

            using var context = contextFactory.CreateDbContext();
            var behavior = context.Behaviors.AsNoTracking().Where(b => b.Name.Equals(behaviorName)).Include(b => b.BehaviorSteps).First();


            Assert.AreEqual(behaviorName, behavior.Name);
            Assert.NotNull(behavior.BehaviorSteps);
            Assert.AreEqual(stepNums, behavior.BehaviorSteps.Count);

            var steps = behavior.BehaviorSteps.Select(bs => bs.StepNum).OrderBy(s => s).ToArray();
            for (int i = 0; i < stepNums; ++i)
                Assert.AreEqual(i + 1, steps[i]);
        }

        [Test]
        [TestCase(5, "test")]
        public void CheckSteps(int stepNums, string behaviorName)
        {
            List<(int x, int y)> coords = new();
            for (int i = 0; i < stepNums; ++i)
            {
                coords.Add((i, i));
            }

            var loader = new BehaviorLoaderService(contextFactory);
            loader.Load(behaviorName, coords);

            using var context = contextFactory.CreateDbContext();
            var behavior = context.Behaviors.AsNoTracking().Where(b => b.Name.Equals(behaviorName)).Include(b => b.BehaviorSteps).First();


            Assert.AreEqual(behaviorName, behavior.Name);
            Assert.NotNull(behavior.BehaviorSteps);
            Assert.AreEqual(stepNums, behavior.BehaviorSteps.Count);

            var steps = behavior.BehaviorSteps.Select(bs => (bs.X, bs.Y)).ToList();
            
            
                Assert.AreEqual(coords, steps);
        }
    }
}
