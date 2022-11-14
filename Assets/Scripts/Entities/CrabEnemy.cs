using Controllers;
using Controllers.NavMesh;
using Controllers.Utils;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(LifeController))]
    public class CrabEnemy : Enemy
    {
        private Cooldown _attackCooldown = new Cooldown();
        private bool _isAttacking = false;

        new void OnEnable()
        {
            base.OnEnable();

            _enemyFollowController = GetComponent<EnemyFollowController>();

            base.SoundController = GetComponent<SoundController>();
            
            // TODO: DIE SOUND and maybe a WALKING ONE?
        }

        void Update()
        {
            // if enemy is attacking, do nothing as it's exploding
            if (_isAttacking)
                _enemyFollowController.MoveToPlayer = false;

            float distanceFromPlayer = _enemyFollowController.getDistanceFromPlayer();
            
            if (distanceFromPlayer < 0.5f) // TODO: HARDCODED
                Attack();
        }

        public override void Attack()
        {
            // add a cooldown to give the player time to react
            if (_attackCooldown.IsOnCooldown()) return;
            
            GetComponent<Animator>().SetTrigger("Sleep");
            _isAttacking = true;
            StartCoroutine(_attackCooldown.CallbackCooldown(this.Stats.AttackCooldown, FixedAttack));
        }

        private void FixedAttack()
        {
            // detect if the player is in range
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, this.EnemyStats.AttackRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Player"))
                {
                    bool hitCrit = Random.Range(0, 1) < this.Stats.CritProbability;
                    hitCollider.gameObject.GetComponent<LifeController>().TakeDamage(this.Stats.Damage, hitCrit);
                    break;
                }
            }

            Die();
        }
    }
}