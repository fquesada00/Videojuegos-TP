using Strategies;

namespace Commands.Animations
{
    public class CmdAttackAnimation : ICommand
    {
        private IAnimatable _animatable;
        private IWeapon _weapon;
        
        public CmdAttackAnimation(IAnimatable animatable, IWeapon weapon)
        {
            _animatable = animatable;
            _weapon = weapon;
        }
        public void Execute()
        {
            _animatable.Attack();
        }
    }
}