using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour, IMoveable
{
    public float Speed => 10;
    //public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float RotationSpeed => 30;
    // public float RotationSpeed => GetComponent<Actor>().ActorStats.RotationSpeed;

    public void Travel(Vector3 direction)
    {
        transform.Translate(direction * (Time.deltaTime * Speed));
    }

    public void Rotate(Vector3 direction)
    {
        Vector3 lookDirection = direction.normalized;
        var freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
        var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
        var eulerY = transform.eulerAngles.y;

        if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
        var euler = new Vector3(0, eulerY, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), 10 * Time.deltaTime);
    }
}
