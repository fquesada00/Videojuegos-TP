using Strategies;
using UnityEngine;

public class CmdDash : ICommand
{
    private readonly IMoveable _moveable;

    public CmdDash(IMoveable moveable)
    {
        _moveable = moveable;
    }


    public void Execute()
    {
        _moveable.Dash();
    }
}