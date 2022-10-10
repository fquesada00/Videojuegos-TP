using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Strategies;
using UnityEngine;
using Controllers.Utils;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Wand : Weapon
{
    public SwordStats SwordStats => _swordStats;
    [SerializeField] private SwordStats _swordStats;


    private Rigidbody _rigidbody;

    private Collider _collider;

    private Cooldown _attackCooldown;
    private float Damage => _swordStats.Damage;

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
        _collider.enabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _collider.enabled = false;
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(Damage);
        }
    }
}