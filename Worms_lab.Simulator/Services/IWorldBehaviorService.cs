using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.Simulator.Services
{
    public interface IWorldBehaviorService
    {
        void Load(string behaviorName);
        Food GetFoodOnStep(int stepNumber);

    }
}
