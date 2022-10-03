using System;
using System.Collections;
using System.Collections.Generic;
using Controllers.Utils;
using UnityEngine;

public class MovementController : MonoBehaviour, IMoveable
{
    // Instancias
    private Cooldown _dashCooldown = new Cooldown();
    public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float JumpHeight => GetComponent<Actor>().ActorStats.JumpHeight;
    public float RotationSmoothSpeed => GetComponent<Actor>().ActorStats.RotationSmoothSpeed;
    public int MaxContinuosJumps => GetComponent<Actor>().ActorStats.MaxContinuosJumps;

    protected int _currentContinuosJumps = 0;
    public int CurrentContinuosJumps { get; }

    public float DashSpeedMultiplier => GetComponent<Actor>().ActorStats.DashSpeedMultiplier;

    private float _turnSmoothVelocity;
    public float DashCooldown => GetComponent<Actor>().ActorStats.DashCooldown;

    public void Travel(Vector3 direction)
    {
        transform.Translate(direction.normalized * (Time.deltaTime * Speed), Space.World);
    }

    public void Rotate(float angle)
    {
        float smoothedAngle =
            Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref _turnSmoothVelocity, RotationSmoothSpeed);
        transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
    }

    public void Jump()
    {
        if (_currentContinuosJumps < MaxContinuosJumps)
        {
            _currentContinuosJumps++;
            GetComponent<Rigidbody>().AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
        }
    }

    public void ResetJumpsCounter()
    {
        _currentContinuosJumps = 0;
    }

    public void Dash()
    {
        if (_dashCooldown.IsOnCooldown()) return;
        
        GetComponent<Rigidbody>().AddForce(transform.forward * (Speed * DashSpeedMultiplier), ForceMode.Impulse);
        StartCoroutine(_dashCooldown.BooleanCooldown(DashCooldown));
    }
}