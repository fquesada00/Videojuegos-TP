using Strategies;

namespace Commands.Animations
{
    public class CmdStartRunAnimation : ICommand
    {
        private IAnimatable _animatable;
        
        public CmdStartRunAnimation(IAnimatable animatable)
        {
            _animatable = animatable;
        }
        public void Execute()
        {
            _animatable.StartRun();
        }
    }
}