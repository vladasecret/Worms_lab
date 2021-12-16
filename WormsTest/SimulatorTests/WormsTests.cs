using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Worms_lab.Simulator;
using Worms_lab.Simulator.Services;
using Worms_lab.Simulator.Strategies;

namespace WormsTest.SimulatorTests
{
    public class WorldSimulatorTests
    {
        private WorldSimulator simulator = 
            new WorldSimulator(new WorldStateWriterService(Console.Out), new NormalFoodGeneratorService(), new NameGeneratorService());


        [Test]
        [TestCase(Direction.UP)]
        [TestCase(Direction.DOWN)]
        [TestCase(Direction.RIGHT)]
        [TestCase(Direction.LEFT)]
        public void MoveToEmptyCell(Direction direction)
        {
            var stabBehavior = new Mock<IBehaviorStrategy>();
            stabBehavior.Setup(behavior => behavior.GetIntention(It.IsAny<Worm>()))
                .Returns((direction, false));

            WorldState state = new WorldState();
            Worm worm = new Worm(state.AsReadOnly(), stabBehavior.Object, "Bob");
            state.Worms.Add(worm);

            simulator.InitState(state);

            simulator.ValidateIntentions();

            Assert.AreEqual(worm.Position, direction.GetPosition(), $"Worm {worm.Name} should move {Enum.GetName(typeof(Direction), direction)}");
        }

        [Test]
        [TestCase(Direction.UP)]
        [TestCase(Direction.DOWN)]
        [TestCase(Direction.RIGHT)]
        [TestCase(Direction.LEFT)]
        public void MoveToFood(Direction direction)
        {
            var stabBehavior = new Mock<IBehaviorStrategy>();
            stabBehavior.Setup(behavior => behavior.GetIntention(It.IsAny<Worm>()))
                .Returns((direction, false));

            WorldState state = new WorldState();
            Worm worm = new Worm(state.AsReadOnly(), stabBehavior.Object, "Bob");
            state.Worms.Add(worm);
            state.Food.Add(new Food(direction.GetPosition()));

            simulator.InitState(state);

            simulator.ValidateIntentions();

            Assert.AreEqual(0, state.Food.Count, "Food must be eaten");
            Assert.AreEqual(worm.Health, 19, "Food should be increase health by 10");
        }

        [Test]
        [TestCase(Direction.UP)]
        [TestCase(Direction.DOWN)]
        [TestCase(Direction.RIGHT)]
        [TestCase(Direction.LEFT)]
        public void MoveToWorm(Direction direction)
        {
            var stabBehavior1 = new Mock<IBehaviorStrategy>();
            stabBehavior1.Setup(behavior => behavior.GetIntention(It.IsAny<Worm>()))
                .Returns((direction, false));

            var stabBehavior2 = Mock.Of<IBehaviorStrategy>(b => b.GetIntention(It.IsAny<Worm>()) == null);

            WorldState state = new WorldState();
            Worm worm1 = new Worm(state.AsReadOnly(), stabBehavior1.Object, "Bob");
            Worm worm2 = new Worm(state.AsReadOnly(), stabBehavior2, "Sam", direction.GetPosition());
            state.Worms.Add(worm1);
            state.Worms.Add(worm2);

            simulator.InitState(state);

            simulator.ValidateIntentions();

            Assert.AreEqual(worm1.Position, new Position(0, 0));
        }


        [Test]
        [TestCase(Direction.UP)]
        [TestCase(Direction.DOWN)]
        [TestCase(Direction.RIGHT)]
        [TestCase(Direction.LEFT)]
        public void SuccessfulSplit(Direction direction)
        {
            var stabBehavior1 = new Mock<IBehaviorStrategy>();
            stabBehavior1.Setup(behavior => behavior.GetIntention(It.IsAny<Worm>()))
                .Returns((direction, true));

            WorldState state = new WorldState();
            Worm worm = new Worm(state.AsReadOnly(), stabBehavior1.Object, "Bob");
            worm.EatFood();                                                 //Heath > 10 for success

            state.Worms.Add(worm);
            simulator.InitState(state);

            simulator.ValidateIntentions();

            Assert.AreEqual(state.Worms.Count, 2);
            Assert.AreEqual(worm.Health, 9, "Worm should lose 10 health");
        }


        [Test]
        [TestCase(Direction.UP)]
        [TestCase(Direction.DOWN)]
        [TestCase(Direction.RIGHT)]
        [TestCase(Direction.LEFT)]
        public void LowHeathToSplit(Direction direction)
        {
            var stabBehavior1 = new Mock<IBehaviorStrategy>();
            stabBehavior1.Setup(behavior => behavior.GetIntention(It.IsAny<Worm>()))
                .Returns((direction, true));

            WorldState state = new WorldState();
            Worm worm = new Worm(state.AsReadOnly(), stabBehavior1.Object, "Bob");

            state.Worms.Add(worm);
            simulator.InitState(state);

            simulator.ValidateIntentions();

            Assert.AreEqual(state.Worms.Count, 1, "Worm should not split with heath 10");
        }

        [Test]
        [TestCase(Direction.UP)]
        [TestCase(Direction.DOWN)]
        [TestCase(Direction.RIGHT)]
        [TestCase(Direction.LEFT)]
        public void SplitToLet(Direction direction)
        {
            var stabBehavior = new Mock<IBehaviorStrategy>();
            stabBehavior.Setup(behavior => behavior.GetIntention(It.IsAny<Worm>()))
                .Returns((direction, true));

            WorldState state = new WorldState();
            Worm worm = new Worm(state.AsReadOnly(), stabBehavior.Object, "Bob");

            state.Worms.Add(worm);
            simulator.InitState(state);

            state.Food.Add(new Food(direction.GetPosition()));

            simulator.ValidateIntentions();

            Assert.AreEqual(state.Worms.Count, 1, "Worm cannot appear at the place of food");

            state.Food.Clear();
            state.Worms.Add(new Worm(state.AsReadOnly(), stabBehavior.Object, "Sam", direction.GetPosition()));

            simulator.ValidateIntentions();

            Assert.AreEqual(state.Worms.Count, 2, "Worm cannot appear at the place of another worm");
        }


        [Test]
        public void GenerateFoodOnWorm()
        {
            var stabBehavior = Mock.Of<IBehaviorStrategy>(bs => bs.GetIntention(It.IsAny<Worm>()) == null);
            WorldState state = new WorldState();
            Worm worm = new Worm(state.AsReadOnly(), stabBehavior, "Bob");
            state.Worms.Add(worm);
            var foodGenerator = Mock.Of<IFoodGeneratorService>(fg => fg.Generate(It.IsAny<ReadOnlyState>()) == new Food(new Position()));
            var simulator = new WorldSimulator(new WorldStateWriterService(Console.Out), foodGenerator, new NameGeneratorService());
            simulator.InitState(state);

            simulator.MakeStep();

            Assert.AreEqual(19, worm.Health);
            Assert.AreEqual(0, state.Food.Count);
        }


    }
}