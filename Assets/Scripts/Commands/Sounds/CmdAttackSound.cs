using Controllers;
using Strategies;

namespace Commands.Sounds
{
    public class CmdAttackSound : ICommand
    {
        private IListenable _listenable;
        
        public CmdAttackSound(IListenable listenable)
        {
            _listenable = listenable;
        }
        
        public void Execute()
        {
            _listenable.PlayOnShot(SoundType.ATTACK);
        }
    }
}