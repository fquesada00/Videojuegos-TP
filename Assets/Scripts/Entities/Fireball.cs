using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using Strategies;
public class Fireball : Bullet
{
    public new void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(_collisionTag))
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage, Crit);
                Destroy(gameObject);
            }
        }
}
