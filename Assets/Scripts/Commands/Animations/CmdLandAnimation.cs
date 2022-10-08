using Strategies;

namespace Commands.Animations
{
    public class CmdLandAnimation : ICommand
    {
        private IAnimatable _animatable;
        
        public CmdLandAnimation(IAnimatable animatable)
        {
            _animatable = animatable;
        }
        public void Execute()
        {
            _animatable.Land();
        }
    }
}