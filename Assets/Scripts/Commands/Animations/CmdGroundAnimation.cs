using Strategies;

namespace Commands.Animations
{
    public class CmdGroundAnimation : ICommand
    {
        private IAnimatable _animatable;
        private bool  _grounded;
        public CmdGroundAnimation(IAnimatable animatable, bool grounded)
        {
            _animatable = animatable;
            _grounded = grounded;
        }
        public void Execute()
        {
            _animatable.setGrounded(_grounded);
        }
    }
}