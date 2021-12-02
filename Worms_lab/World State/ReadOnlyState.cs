using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab
{
    public class ReadOnlyState
    {
        public ReadOnlyCollection<Worm> Worms { get; init; }
        public ReadOnlyCollection<Food> Food { get; init; }

        private readonly WorldState state;

        public ReadOnlyState(WorldState state)
        {
            this.state = state;
            Worms = state.Worms.AsReadOnly();
            Food = state.Food.AsReadOnly();
        }

        public bool IsWorm(Position position)
        {
            return state.Worms.Exists(worm => worm.Position.Equals(position));
        }

        
        public bool IsFood(Position position)
        {
            return state.Food.Exists(item => item.Position.Equals(position));
        }
    }
}
