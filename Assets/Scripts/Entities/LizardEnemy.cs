using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Controllers.Utils;

[RequireComponent(typeof(LifeController))]
public class LizardEnemy : Enemy
{
    private Animator _animator;

    private Cooldown _attackCooldown;
    private static readonly int AttackId = Animator.StringToHash("attack");

    void Start()
    {
        _animator = GetComponent<Animator>();
        _attackCooldown = new Cooldown();
    }

    void Update()
    {
        Attack();
    }

    public override void Attack()
    {
        if (_attackCooldown.IsOnCooldown()) return;
        _animator.SetTrigger(AttackId);
        StartCoroutine(_attackCooldown.BooleanCooldown(this.EnemyStats.AttackCooldown));
    }
}
