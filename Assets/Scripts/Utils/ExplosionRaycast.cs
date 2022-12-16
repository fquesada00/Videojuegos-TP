using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Utils
{
    public class ExplosionRaycast
    {
        public static void Explode(Vector3 position, float range, float damage)
        {
            // detect if the player is in range
            Collider[] hitColliders = Physics.OverlapSphere(position, range);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Player"))
                {
                    // get the distance within the sphere and multiply it by the damage
                    float distance = Vector3.Distance(position, hitCollider.transform.position);
                    
                    // when enemy is touching the player, it does xAttackRange damage
                    // if it's further away, it does less damage
                    // formula = r - d * (1 - 1 / r) where r is the radius and d the distance
                    float rangeDamageMultiplier = range - distance * (1 - (1 / range));
                    hitCollider.gameObject.GetComponent<LifeController>().TakeDamage(damage * rangeDamageMultiplier, false);
                    break;
                }
            }
        }
    }
}
