using Strategies;
using UnityEngine;

public class CmdMovement : ICommand
{
    private readonly Vector3 _direction;
    private readonly IMoveable _moveable;
    

    public CmdMovement(IMoveable moveable, Vector3 direction)
    {
        _moveable = moveable;
        _direction = direction;
    }

    public void Execute()
    {
        _moveable.Travel(_direction);
    }
}