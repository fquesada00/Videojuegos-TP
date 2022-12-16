using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Strategies;
using UnityEngine;
using Utils;

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

    private HashSet<IDamageable> _damageables = new HashSet<IDamageable>();

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

    public void finishAttack(){
        _collider.enabled = false;
        foreach (var damageable in _damageables)
        {
            damageable.TakeDamage(_swordStats.Damage, _crit);
        }
        _damageables.Clear();
    }


    public override void Attack(bool crit)
    {
        base.Attack(crit);
        _crit = crit;
        _collider.enabled = true;
        _damageables.Clear();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_collisionTag))
        {
            var damageable = other.gameObject.GetComponent<IDamageable>();
            if(damageable != null){
                _damageables.Add(damageable);
            }
        }
    }
}
