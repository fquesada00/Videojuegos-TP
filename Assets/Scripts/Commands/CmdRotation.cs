using UnityEngine;

public class CmdRotation : ICommand
{
    private readonly Vector3 _direction;
    private readonly IMoveable _moveable;

    public CmdRotation(IMoveable moveable, Vector3 direction)
    {
        _moveable = moveable;
        _direction = direction;
    }

    public void Execute()
    {
        _moveable.Rotate(_direction);
    }
}