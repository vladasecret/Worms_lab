
namespace Worms_lab.Strategies
{
    interface IBehaviorStrategy
    {
        (Direction Direction, bool Split)? GetIntention(Worm worm);
    }

}
