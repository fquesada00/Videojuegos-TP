using Strategies;

namespace Commands.Animations
{
    public class CmdStopRunAnimation : ICommand
    {
        private IAnimatable _animatable;

        public CmdStopRunAnimation(IAnimatable animatable)
        {
            _animatable = animatable;
        }

        public void Execute()
        {
            if (!_animatable.IsRunning) return;
            _animatable.StopRun();
        }
    }
}