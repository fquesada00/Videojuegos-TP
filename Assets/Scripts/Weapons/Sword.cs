using System;
using System.Collections;
using System.Collections.Generic;
using Strategies;
using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Sword : MonoBehaviour, IWeapon
{
    public SwordStats SwordStats => _swordStats;
    [SerializeField] private SwordStats _swordStats;
    
    public Rigidbody rigidbody => _rigidbody;
    [SerializeField] private Rigidbody _rigidbody;

    public Collider collider => _collider;
    [SerializeField] private Collider _collider;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        
        _collider.isTrigger = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    public void Attack()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(SwordStats.Damage);
        }

    }
}
