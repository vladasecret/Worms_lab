using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worms_lab.Strategies;
using Worms_lab.services;
using System.Collections.ObjectModel;

namespace Worms_lab
{
    class World
    {        
        private readonly List<Worm> worms = new List<Worm>();
        private readonly List<Food> food = new List<Food>();

        private readonly WorldLogger logger;
        private IBehaviorStrategy strategy;
        public World(StreamWriter streamWriter)
        {
            strategy = new CleverMoveStrategy(this);
            logger = new WorldLogger(streamWriter);
            worms.Add(new Worm(strategy));
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
            List<Worm> newWorms = new List<Worm>();
            for (int i = 0; i < 100; ++i)
            {
                GenerateFood();
                logger.Log(worms, food);
                GetIntentions();
                ValidateIntentions(newWorms);
                UpdateFood();
                UpdateWorms();
                if (newWorms.Count != 0)
                {
                    worms.AddRange(newWorms);
                    newWorms.Clear();
                }
            }
            logger.Log(worms, food);
        }

        private void GetIntentions()
        {
            worms.ForEach(worm => worm.makeIntention());
        }

        private void ValidateIntentions(List<Worm> newWorms)
        {
            
            foreach (var worm in worms)
            {
                if (worm.Intention.HasValue)
                {
                    (Direction Direction, bool Split) intention = worm.Intention.Value;
                    switch (intention.Split)
                    {
                        case false:
                            {
                                Position pos = worm.Position + intention.Direction.GetPosition();
                                if (IsWorm(pos))
                                    break;
                                if (IsFood(pos, out Food food))
                                {
                                    worm.EatFood();
                                    this.food.Remove(food);
                                }
                                worm.Step(intention.Direction);
                                break;
                            }

                        case true:
                            {
                                Position pos = worm.Position + intention.Direction.GetPosition();
                                if (worm.Health < 11 || IsFood(pos) || IsWorm(pos))
                                    break;
                                newWorms.Add(worm.Split(intention.Direction));
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
            Position position;
            Random random = new();
            do
            {
                position = new Position(random.NextNormal(0, 5));
            } while (IsFood(position));

            if (IsWorm(position, out Worm worm))
                worm.EatFood();
            else food.Add(new Food(position));
        }
        
        private void UpdateFood()
        {
            food.RemoveAll(item => { item.updateFreshness(); return item.Freshness == 0;});
        }

        private void UpdateWorms()
        {
            worms.RemoveAll(item => { item.UpdateHealth(); return item.Health == 0; });
        }
    }
}
