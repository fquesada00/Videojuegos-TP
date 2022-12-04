using Strategies;
using UnityEngine;

namespace Commands.Movements
{
    public class CmdDash : ICommand
    {
        private readonly IMoveable _moveable;
        private readonly Vector3 _forwardDir;	

        public CmdDash(IMoveable moveable, Vector3 forwardDir)
        {
            _moveable = moveable;
            _forwardDir = forwardDir;
        }


        public void Execute()
        {
            _moveable.Dash(_forwardDir);
        }
    }
}