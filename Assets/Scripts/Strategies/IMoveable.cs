using UnityEngine;

public interface IMoveable
{
    float Speed { get; }
    void Travel(Vector3 direction);
}