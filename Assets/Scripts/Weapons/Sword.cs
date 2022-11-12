using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Strategies;
using UnityEngine;
using Controllers.Utils;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Sword : Weapon
{
    public override WeaponStats WeaponStats => _swordStats;
    public SwordStats SwordStats => _swordStats;
    [SerializeField] private SwordStats _swordStats;

    private Rigidbody _rigidbody;

    private Collider _collider;

    private Cooldown _attackCooldown;

    private new void Start()
    {
        base.Start();
        
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _attackCooldown = new Cooldown();

        _collider.isTrigger = true;
        _collider.enabled = false;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }


    public override void Attack()
    {
        base.Attack();
        _collider.enabled = true;
        // StartCoroutine(
        //  _attackCooldown.CallbackCooldown(SwordStats.AttackCooldown, () =>
        //  {
        //      _collider.enabled = false;
        //      Debug.Log("Attack cooldown ended");
        //  }));

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _collider.enabled = false;
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(this.Damage());
        }
    }
}
