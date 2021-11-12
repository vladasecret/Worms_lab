using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab
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

        public void updateFreshness()
        {
            Freshness--;
        }

        public override string ToString()
        {
            //return Position.ToString();
            return $"{Position}";
        }
    }
}
