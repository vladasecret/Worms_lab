using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worms_lab.Strategies;
using Worms_lab.services;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace Worms_lab
{
    public class WorldSimulator
    {
        private WorldState state;

        private readonly WorldStateWriter stateWriter;
        private readonly FoodGenerator foodGenerator;
        private readonly NameGenerator nameGenerator;

        public WorldSimulator(WorldStateWriter writer, FoodGenerator foodGenerator, NameGenerator nameGenerator)
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
            List<Worm> newWorms = new();

            GenerateFood();
            ValidateIntentions(newWorms);
            state.UpdateFood();
            state.UpdateWorms();
            if (newWorms.Count != 0)
            {
                state.Worms.AddRange(newWorms);
                newWorms.Clear();
            }
            stateWriter.Log(state);
        }

        public void ValidateIntentions(List<Worm> newWorms)
        {
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
