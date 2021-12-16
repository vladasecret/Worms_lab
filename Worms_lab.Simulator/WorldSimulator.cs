using System.Collections.Generic;
using Worms_lab.Simulator.Services;


namespace Worms_lab.Simulator
{
    public class WorldSimulator
    {
        private WorldState state;

        private readonly WorldStateWriterService stateWriter;
        private readonly IFoodGeneratorService foodGenerator;
        private readonly NameGeneratorService nameGenerator;

        public WorldSimulator(WorldStateWriterService writer, IFoodGeneratorService foodGenerator, NameGeneratorService nameGenerator)
        {
            stateWriter = writer;
            this.foodGenerator = foodGenerator;
            this.nameGenerator = nameGenerator;
        }

        public void InitState(WorldState state)
        {
            this.state = state;
        }

        public void MakeStep()
        {            
            GenerateFood();
            ValidateIntentions();
            state.UpdateFood();
            state.UpdateWorms();
            stateWriter.Log(state);
        }

        public void ValidateIntentions()
        {
            List<Worm> newWorms = new List<Worm>();
            (Direction direction, bool split)? intention;
            foreach (var worm in state.Worms)
            {
                intention = worm.GetIntention();
                if (intention.HasValue)
                {
                    switch (intention.Value.split)
                    {
                        case false:
                            {
                                Position pos = worm.Position + intention.Value.direction.GetPosition();
                                if (state.IsWorm(pos))
                                    break;
                                if (state.IsFood(pos, out Food food))
                                {
                                    worm.EatFood();
                                    state.Food.Remove(food);
                                }
                                worm.Step(intention.Value.direction);
                                break;
                            }

                        case true:
                            {
                                Position pos = worm.Position + intention.Value.direction.GetPosition();
                                if (worm.Health < 11 || state.IsFood(pos) || state.IsWorm(pos))
                                    break;
                                newWorms.Add(worm.Split(intention.Value.direction, nameGenerator.GenerateName()));
                                break;
                            }

                    }
                }
                worm.UpdateHealth();
            }
            if (newWorms.Count != 0)
            {
                state.Worms.AddRange(newWorms);
            }
        }

        public void GenerateFood()
        {
            Food foodItem = foodGenerator.Generate(state.AsReadOnly());

            if (state.IsWorm(foodItem.Position, out Worm worm))
                worm.EatFood();
            else state.Food.Add(foodItem);
        }

    }
}
