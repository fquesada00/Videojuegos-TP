using Strategies;

namespace Commands.Animations
{
    public class CmdJumpAnimation : ICommand
    {
        private IAnimatable _animatable;
        
        public CmdJumpAnimation(IAnimatable animatable)
        {
            _animatable = animatable;
        }
        public void Execute()
        {
            _animatable.Jump();
        }
    }
}