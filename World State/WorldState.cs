using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab
{
    public class WorldState
    {
        public List<Worm> Worms {get; init;}
        public List<Food> Food { get; init; }

        public WorldState()
        {
            Worms = new();
            Food = new();
        }
        public bool IsWorm(Position position)
        {
            return Worms.Exists(worm => worm.Position.Equals(position));
        }

        public bool IsWorm(Position position, out Worm worm)
        {
            worm = Worms.Find(worm => worm.Position.Equals(position));
            return worm != null;
        }
        public bool IsFood(Position position)
        {
            return Food.Exists(item => item.Position.Equals(position));
        }

        public bool IsFood(Position position, out Food foodItem)
        {
            foodItem = Food.Find(item => item.Position.Equals(position));
            return foodItem != null;
        }

        public void UpdateFood()
        {
            Food.RemoveAll(item => { item.updateFreshness(); return item.Freshness == 0; });
        }

        public void UpdateWorms()
        {
            Worms.RemoveAll(item => item.Health == 0 );
        }

        public ReadOnlyState AsReadOnly()
        {
            return new ReadOnlyState(this);
        }
    }
}
