using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Strategies;
using UnityEngine;
using Utils;
using Managers;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Wand : Weapon
{
    public override WeaponStats WeaponStats => _swordStats;
    public SwordStats SwordStats => _swordStats;
    [SerializeField] private SwordStats _swordStats;


    private Rigidbody _rigidbody;

    private Collider _collider;

    private Cooldown _attackCooldown;

    private bool _crit = false;
    
    private float _damageMultiplier = 1f;

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
        
        // find game manager within the scene
        _damageMultiplier = FindObjectOfType<GameManager>().GetCurrentDifficultyStats.EnemyDamageMultiplier;
    }


    public override void Attack(bool crit)
    {
        base.Attack(crit);
        _crit = crit;
        _collider.enabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _collider.enabled = false;
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(SwordStats.Damage * _damageMultiplier, _crit);
        }
    }
}
