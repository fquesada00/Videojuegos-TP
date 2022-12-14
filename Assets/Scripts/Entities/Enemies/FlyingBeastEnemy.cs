using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Utils;
using Entities;

[RequireComponent(typeof(LifeController))]
public class FlyingBeastEnemy : FlyingEnemy
{
    private Animator _animator;

    private Cooldown _attackCooldown;
    private static readonly int AttackId = Animator.StringToHash("attack");

    [SerializeField] private FireballThrower _fireballThrower;

    private PatrolEnemy _patrolEnemy;

    private AudioSource _audioSource;

    private void Start() {
        EventsManager.instance.OnPauseChange += OnPauseChange;    
    }
    new void OnEnable()
    {
        base.OnEnable();
        _animator = GetComponent<Animator>();
        _attackCooldown = new Cooldown();
        _audioSource = GetComponent<AudioSource>();
        float random = Random.Range(0.8f, 1.2f);
        _animator.SetFloat("animationSpeed", random);
        _patrolEnemy = GetComponent<PatrolEnemy>();
        _audioSource.pitch = random;
    }

    new void Update()
    {
        base.Update();
        if (IsOnEnemyRange())
        {
            var position = _player.transform.position;
            transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
            _fireballThrower.Target = _player.gameObject;
            Attack();
        }
    }

    public override void Attack()
    {
        if (_attackCooldown.IsOnCooldown()) return;
        _animator.SetTrigger(AttackId);
        _fireballThrower.Attack(false);
        StartCoroutine(_attackCooldown.BooleanCooldown(this.EnemyStats.AttackCooldown));
    }

    public void OnPauseChange(bool isPaused)
    {
        if (isPaused)
        {
            _audioSource.Pause();
        } else
        {
            _audioSource.UnPause();
        }
    }
}
