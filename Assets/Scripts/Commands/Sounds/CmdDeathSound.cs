using Controllers;
using Strategies;

namespace Commands.Sounds
{
    public class CmdDeathSound : ICommand
    {
        private IListenable _listenable;
        
        public CmdDeathSound(IListenable listenable)
        {
            _listenable = listenable;
        }
        
        public void Execute()
        {
            _listenable.PlayOnShot(SoundType.DEATH);
        }
    }
}