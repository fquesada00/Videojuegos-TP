using UnityEngine;

public interface IMoveable
{
    float Speed { get; }
    float RotationSpeed { get; }
    void Travel(Vector3 direction);
    void Rotate(Vector3 direction);
}