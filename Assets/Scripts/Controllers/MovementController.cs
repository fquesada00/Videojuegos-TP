using System;
using System.Collections;
using System.Collections.Generic;
using Controllers.Utils;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour, IMoveable
{
    [SerializeField] private CharacterController _characterController;
    private Cooldown _dashCooldown = new Cooldown();
    private float _ySpeed = 0f;

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

    private void Update() {
        if(_ySpeed < 0 && _characterController.isGrounded) {
            _currentContinuosJumps = 0;
            _ySpeed = 0;
        }else{
            _ySpeed += Physics.gravity.y * Time.deltaTime;
            _characterController.Move(Vector3.up * _ySpeed * Time.deltaTime);
            Debug.Log("FALL");
        }
        Debug.Log("FALLnt");

    }

    public bool IsGrounded()
    {
        return _characterController.isGrounded;
    }

    public void Travel(Vector3 direction)
    {
        _characterController.Move(direction * Speed * Time.deltaTime);
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
            Debug.Log("Jump");
            _currentContinuosJumps++;
            _ySpeed += Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
            Debug.Log(_ySpeed);
        }
    }

    public void Dash(Vector3 forwardDir)
    {
        if (_dashCooldown.IsOnCooldown()) return;
        
        EventsManager.instance.EventSkillCooldownChange(1, DashCooldown);
        _characterController.Move(forwardDir * Speed * 25* DashSpeedMultiplier * Time.deltaTime);
        StartCoroutine(_dashCooldown.BooleanCooldown(DashCooldown));
    }
}