using UnityEngine;

public interface IMoveable
{
    float Speed { get; }
    float RotationSmoothSpeed { get; }
    void Travel(Vector3 direction);
    void Rotate(float angle);
    void Jump();
}