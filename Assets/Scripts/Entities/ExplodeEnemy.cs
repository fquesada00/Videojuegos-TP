using Controllers;
using Controllers.NavMesh;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(LifeController))]
    public class ExplodeEnemy : Enemy
    {
        new void OnEnable()
        {
            base.OnEnable();

            _enemyFollowController = GetComponent<EnemyFollowController>();
        
            // GetComponent<Animator>().SetBool("idle_combat", true);

            base.SoundController = GetComponent<SoundController>();
        
        }
        
        void Update()
        {
            float distanceFromPlayer = _enemyFollowController.getDistanceFromPlayer();
            if (distanceFromPlayer < this.EnemyStats.AttackRange)
            {
                Attack();
            }
        }
        
        public override void Attack()
        {
            Debug.Log("Explode");
        }
    }
}