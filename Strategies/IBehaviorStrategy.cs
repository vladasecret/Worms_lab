
namespace Worms_lab.Strategies
{
    public interface IBehaviorStrategy
    {
        (Direction Direction, bool Split)? GetIntention(Worm worm);
    }

}
