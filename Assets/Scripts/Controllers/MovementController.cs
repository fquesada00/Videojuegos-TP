using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour, IMoveable
{
    public float Speed => 10;
    //public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;

    public void Travel(Vector3 direction)
    {
        transform.Translate(direction * (Time.deltaTime * Speed));
    }
}
