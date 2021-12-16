using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.Simulator
{
    public class Food
    {
        public Position Position { get; private set;}

        public int Freshness { get; private set; }

        public Food(Position pos)
        {
            Position = pos;
            Freshness = 10;
        }

        public Food(Position pos, int freshness)
        {
            Position = pos;
            Freshness = freshness;
        }

        public void UpdateFreshness()
        {
            Freshness--;
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
                return true;
            if (obj is Food)
            {
                var food = (Food)obj;
                return food.Position.Equals(Position);
            }
            return false;
        }
        public override string ToString()
        {
            //return Position.ToString();
            return $"{Position}";
        }
    }
}
