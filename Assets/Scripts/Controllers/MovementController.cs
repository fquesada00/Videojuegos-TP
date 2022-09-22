using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour, IMoveable
{
    public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float RotationSmoothSpeed => GetComponent<Actor>().ActorStats.RotationSmoothSpeed;
    private float _turnSmoothVelocity;
    public void Travel(Vector3 direction)
    {
        transform.Translate(direction.normalized * (Time.deltaTime * Speed), Space.World);
    }

    public void Rotate(float angle)
    {
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref _turnSmoothVelocity, RotationSmoothSpeed);
        transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
    }
}
