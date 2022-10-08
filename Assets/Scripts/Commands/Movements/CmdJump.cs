using Strategies;
using UnityEngine;

public class CmdJump : ICommand
{
    private readonly IMoveable _moveable;

    public CmdJump(IMoveable moveable)
    {
        _moveable = moveable;
    }

    public void Execute()
    {
        _moveable.Jump();
    }
}