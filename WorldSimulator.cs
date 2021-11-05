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
    class WorldSimulator :IHostedService
    {        
        private readonly List<Worm> worms = new List<Worm>();
        private readonly List<Food> food = new List<Food>();

        private readonly WorldStateWriter stateWriter;
        private readonly FoodGenerator foodGenerator;
        private readonly NameGenerator nameGenerator;

        public WorldSimulator(IBehaviorStrategy strategy, WorldStateWriter writer, FoodGenerator foodGenerator, NameGenerator nameGenerator)
        {
            stateWriter = writer;
            this.foodGenerator = foodGenerator;
            this.nameGenerator = nameGenerator;
            worms.Add(new Worm(this, strategy, nameGenerator.GenerateName()));
        }

       
        public ReadOnlyCollection<Worm> GetWorms()
        {
            return worms.AsReadOnly();
        }

        public ReadOnlyCollection<Food> GetFood()
        {
            return food.AsReadOnly();
        }

        public void Live()
        {
            List<Worm> newWorms = new();
            for (int i = 0; i < 100; ++i)
            {
                GenerateFood();
                stateWriter.Log(worms, food);
                ValidateIntentions(newWorms);
                UpdateFood();
                UpdateWorms();
                if (newWorms.Count != 0)
                {
                    worms.AddRange(newWorms);
                    newWorms.Clear();
                }
            }
            stateWriter.Log(worms, food);
        }

        

        private void ValidateIntentions(List<Worm> newWorms)
        {
            (Direction direction, bool split)? intention;
            foreach (var worm in worms)
            {
                intention = worm.GetIntention();
                if (intention.HasValue)
                {
                    switch (intention.Value.split)
                    {
                        case false:
                            {
                                Position pos = worm.Position + intention.Value.direction.GetPosition();
                                if (IsWorm(pos))
                                    break;
                                if (IsFood(pos, out Food food))
                                {
                                    worm.EatFood();
                                    this.food.Remove(food);
                                }
                                worm.Step(intention.Value.direction);
                                break;
                            }

                        case true:
                            {
                                Position pos = worm.Position + intention.Value.direction.GetPosition();
                                if (worm.Health < 11 || IsFood(pos) || IsWorm(pos))
                                    break;
                                newWorms.Add(worm.Split(intention.Value.direction, nameGenerator.GenerateName()));
                                break;
                            }

                    }
                }
            }

        }


        public bool IsWorm(Position position)
        {
            return worms.Exists(worm => worm.Position.Equals(position));
        }

        private bool IsWorm(Position position, out Worm worm)
        {
            worm = worms.Find(worm => worm.Position.Equals(position));
            return worm != null;
        }
        public bool IsFood(Position position)
        {
            return food.Exists(item => item.Position.Equals(position));
        }

        private bool IsFood(Position position, out Food foodItem)
        {
            foodItem = food.Find(item => item.Position.Equals(position));
            return foodItem != null;
        }

        public void GenerateFood()
        {
            Food foodItem = foodGenerator.Generate(this);

            if (IsWorm(foodItem.Position, out Worm worm))
                worm.EatFood();
            else food.Add(foodItem);
        }
        
        private void UpdateFood()
        {
            food.RemoveAll(item => { item.updateFreshness(); return item.Freshness == 0;});
        }

        private void UpdateWorms()
        {
            worms.RemoveAll(item => { item.UpdateHealth(); return item.Health == 0; });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            
            Task.Run(()=>Live());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    } 
}
