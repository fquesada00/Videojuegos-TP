using Strategies;

namespace Commands.Animations
{
    public class CmdRunAnimation : ICommand
    {
        private IAnimatable _animatable;
        private float _speed;
        
        public CmdRunAnimation(IAnimatable animatable, float speed)
        {
            _animatable = animatable;
            _speed = speed;
        }
        public void Execute()
        {
            _animatable.Run(_speed);

        }
    }
}