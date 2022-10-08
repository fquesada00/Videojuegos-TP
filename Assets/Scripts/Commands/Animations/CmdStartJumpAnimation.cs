using Strategies;

namespace Commands.Animations
{
    public class CmdStartJumpAnimation : ICommand
    {
        private IAnimatable _animatable;
        
        public CmdStartJumpAnimation(IAnimatable animatable)
        {
            _animatable = animatable;
        }
        public void Execute()
        {
            _animatable.StartJump();
        }
    }
}