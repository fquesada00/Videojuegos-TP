using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Controllers.Utils;
using Entities;

[RequireComponent(typeof(LifeController))]
public class LizardEnemy : PatrolEnemy
{
    private Animator _animator;

    private Cooldown _attackCooldown;
    private static readonly int AttackId = Animator.StringToHash("attack");
    private static readonly int IdleId = Animator.StringToHash("idle");
    private static readonly int MoveId = Animator.StringToHash("move");

    [SerializeField] private FireballThrower _fireballThrower;


    private PatrolEnemy _patrolEnemy;
    
    new void OnEnable()
    {
        base.OnEnable();
        _animator = GetComponent<Animator>();
        _attackCooldown = new Cooldown();
        
        _patrolEnemy = GetComponent<PatrolEnemy>();
    }

    new void Update()
    {
        base.Update();
        if(_patrolEnemy.IsOnEnemyRange())
        {
            _animator.SetFloat("speed", NavMeshAgent.velocity.magnitude);
            var position = _player.transform.position;
            transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
            _fireballThrower.Target = _player.gameObject;
            Attack();
        } else
            _animator.SetFloat("speed", 1);
    }

    public override void Attack()
    {
        if (_attackCooldown.IsOnCooldown()) return;
        _animator.SetTrigger(AttackId);
        _fireballThrower.Attack(false);
        StartCoroutine(_attackCooldown.BooleanCooldown(this.EnemyStats.AttackCooldown));
    }
}
