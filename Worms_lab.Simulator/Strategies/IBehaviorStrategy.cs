
namespace Worms_lab.Simulator.Strategies
{
    public interface IBehaviorStrategy
    {
        (Direction Direction, bool Split)? GetIntention(Worm worm);
    }

}
