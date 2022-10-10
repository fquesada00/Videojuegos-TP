using Controllers;
using Strategies;

namespace Commands.Sounds
{
    public class CmdSoundAttack : ICommand
    {
        private IListenable _listenable;
        
        public CmdSoundAttack(IListenable listenable)
        {
            _listenable = listenable;
        }
        
        public void Execute()
        {
            _listenable.PlayOnShot(SoundType.ATTACK);
        }
    }
}