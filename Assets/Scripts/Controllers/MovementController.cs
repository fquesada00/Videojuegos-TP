using System;
using System.Collections;
using System.Collections.Generic;
using Controllers.Utils;
using Strategies;
using UnityEngine;
using Constants;

[RequireComponent(typeof(CharacterController), typeof(GameObject))]
public class MovementController : MonoBehaviour, IMoveable
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GameObject _dashVisualEffects;
    private Cooldown _dashCooldown = new Cooldown();
    private float _ySpeed = 0f;
    private float DASH_EFFECTS_DURATION = 0.5f;
    public float SpeedMultiplier {get; set;} = 1f;
    public float Speed => GetComponent<Actor>().ActorStats.BaseMovementSpeed * SpeedMultiplier;
    public float Damage => GetComponent<Actor>().ActorStats.Damage;
    public float JumpHeight => GetComponent<Actor>().ActorStats.JumpHeight;
    public float RotationSmoothSpeed => GetComponent<Actor>().ActorStats.RotationSmoothSpeed;
    public int MaxContinuosJumps => GetComponent<Actor>().ActorStats.MaxContinuosJumps;
    public int CurrentContinuosJumps { get; }
    public float DashPower => GetComponent<Actor>().ActorStats.DashPower;
    public float DashCooldown => GetComponent<Actor>().ActorStats.DashCooldown;
    protected int _currentContinuosJumps = 0;
    private float _turnSmoothVelocity;

    private void Start() {
        foreach (ParticleSystem particleSystem in _dashVisualEffects.GetComponentsInChildren<ParticleSystem>())
        {
            ParticleSystem.MainModule main = particleSystem.main;
            main.duration = DASH_EFFECTS_DURATION;
        }
    }

    private void Update() {
        if(_characterController.isGrounded && _ySpeed < 0) {
            // Reset the number of jumps and yspeed only if the player is grounded and yspeed is negative because it enables the player to jump when speed is >=0
            _currentContinuosJumps = 0;
            _ySpeed = 0;
        }else{
            _ySpeed += Physics.gravity.y * Time.deltaTime;
            _characterController.Move(Vector3.up * _ySpeed * Time.deltaTime);
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.3f);
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
            _currentContinuosJumps++;
            if (_ySpeed < 0) _ySpeed = 0;
            _ySpeed += Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
        }
    }

    public void Dash(Vector3 forwardDir)
    {
        if (_dashCooldown.IsOnCooldown()) return;
        
        EventsManager.instance.EventSkillCooldownChange(0, DashCooldown);
        __DashVisualEffects();
        __DashDamage(forwardDir);

        _ySpeed = Mathf.Sqrt(-2f * Physics.gravity.y) * forwardDir.normalized.y;
        StartCoroutine(_dashCooldown.BooleanCooldown(DashCooldown));
    }

    private void __DashDamage(Vector3 dir)
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, 1, transform.forward, DashPower);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            GameObject gameObject = hit.transform.gameObject;
            // Check if it is an enemy
            if (gameObject.CompareTag("Enemy"))
            {
                IDamageable damageable = gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage*3, false); 
            }
        }

        _characterController.Move(dir * DashPower);
    }

    private void __DashVisualEffects(){
        // activate all dash visual effects
        foreach (ParticleSystem particleSystem in _dashVisualEffects.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Play();
        }
        foreach (var light in _dashVisualEffects.GetComponentsInChildren<Light>())
        {
            light.enabled = true;
            StartCoroutine(new Cooldown().CallbackCooldown(DASH_EFFECTS_DURATION, () => light.enabled = false));
        }
    }

    //on collision enter
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("OutOfBounds")) {
            Vector3 pos = EnemySpawnManager.GetRandomPositionOnNavMesh(transform.position, 10);
            transform.position = pos;
        }
    }

    public void AddYSpeed(float ySpeed)
    {
        _ySpeed += ySpeed;
    }
}
