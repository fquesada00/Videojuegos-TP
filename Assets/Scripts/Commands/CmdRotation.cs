using UnityEngine;

public class CmdRotation : ICommand
{
    private readonly float _angle;
    private readonly IMoveable _moveable;

    public CmdRotation(IMoveable moveable, float angle)
    {
        _moveable = moveable;
        _angle = angle;
    }

    public void Execute()
    {
        _moveable.Rotate(_angle);
    }
}