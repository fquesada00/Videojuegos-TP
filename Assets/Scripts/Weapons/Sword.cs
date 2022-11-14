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

    private bool _crit = false;
    
    private String _collisionTag = "Enemy";

    public String CollisionTag
    {
        get => _collisionTag;
        set => _collisionTag = value;
    }

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


    public override void Attack(bool crit)
    {
        base.Attack(crit);
        _crit = crit;
        _collider.enabled = true;
        // StartCoroutine(
        //  _attackCooldown.CallbackCooldown(SwordStats.AttackCooldown, () =>
        //  {
        //      _collider.enabled = false;
        //      Debug.Log("Attack cooldown ended");
        //  }));
        StartCoroutine(new Cooldown().CallbackCooldown(1, () =>
        {
            _collider.enabled = false;
        }));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_collisionTag))
        {
            _collider.enabled = false;
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(SwordStats.Damage, _crit);
        }
    }
}
