using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.Simulator.Services
{
    public class PreloadFoodGeneratorService : IFoodGeneratorService
    {
        private IWorldBehaviorService worldBehaviorService;
        private int stepNum = 1;
        public PreloadFoodGeneratorService(IWorldBehaviorService worldBehaviorService, BehaviorNameOption behaviorNameOption)
        {
            this.worldBehaviorService = worldBehaviorService;
            worldBehaviorService.Load(behaviorNameOption.Name);
        }

        public Food Generate(ReadOnlyState state)
        {
            var food = worldBehaviorService.GetFoodOnStep(stepNum);
            if (state.IsFood(food.Position))
                throw new ArgumentException("Loaded food already exists");
            stepNum++;
            return food;
        }

        
    }
}
