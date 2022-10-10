using Controllers;
using Strategies;

namespace Commands.Sounds
{
    public class CmdWhisperSound : ICommand
    {
        private IListenable _listenable;
        
        public CmdWhisperSound(IListenable listenable)
        {
            _listenable = listenable;
        }
        
        public void Execute()
        {
            _listenable.PlayOnShot(SoundType.WHISPER);
        }
    }
}