using Strategies;

namespace Commands.Animations
{
    public class CmdStopJumpAnimation : ICommand
    {
        private IAnimatable _animatable;
        
        public CmdStopJumpAnimation(IAnimatable animatable)
        {
            _animatable = animatable;
        }
        public void Execute()
        {
            _animatable.StopJump();
        }
    }
}