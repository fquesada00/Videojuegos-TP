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

    public int CurrentContinuosJumps { get; }

    public float DashSpeedMultiplier => GetComponent<Actor>().ActorStats.DashSpeedMultiplier;

    public float DashCooldown => GetComponent<Actor>().ActorStats.DashCooldown;
    protected int _currentContinuosJumps = 0;
    private float _turnSmoothVelocity;

    private Rigidbody _rigidbody;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

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
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
        }
    }

    public void ResetJumpsCounter()
    {
        _currentContinuosJumps = 0;
    }

    public void Dash()
    {
        if (_dashCooldown.IsOnCooldown()) return;
        
        _rigidbody.AddForce(transform.forward * (Speed * DashSpeedMultiplier), ForceMode.Impulse);
        StartCoroutine(_dashCooldown.BooleanCooldown(DashCooldown));
    }
}