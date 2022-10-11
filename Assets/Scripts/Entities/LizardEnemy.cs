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


    private PatrolEnemy _patrolEnemy;
    
    void OnEnable()
    {
        base.OnEnable();
        _animator = GetComponent<Animator>();
        _attackCooldown = new Cooldown();
        
        _patrolEnemy = GetComponent<PatrolEnemy>();
    }

    void Update()
    {
        _animator.SetFloat("speed", NavMeshAgent.velocity.magnitude);
        base.Update();
        if(_patrolEnemy.IsOnEnemyRange())
        {
            var position = _player.transform.position;
            transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
            Attack();
        }
    }

    public override void Attack()
    {
        if (_attackCooldown.IsOnCooldown()) return;
        _animator.SetTrigger(AttackId);
        StartCoroutine(_attackCooldown.BooleanCooldown(this.EnemyStats.AttackCooldown));
    }
}
