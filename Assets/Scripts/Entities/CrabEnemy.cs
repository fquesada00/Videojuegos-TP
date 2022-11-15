using System;
using System.Collections.Generic;
using Commands.Sounds;
using Controllers;
using Controllers.NavMesh;
using Controllers.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    [RequireComponent(typeof(LifeController), typeof(GameObject), typeof(GameObject))]
    public class CrabEnemy : Enemy
    {
        private Cooldown _attackCooldown = new Cooldown();
        private bool _isAttacking = false;
        [SerializeField] private GameObject _postExplosionVisualEffects;
        [SerializeField] private GameObject _preExplosionVisualEffects;
        
        private Coroutine _preExplosionCoroutine;
        private Coroutine _postExplosionCoroutine;

        new void OnEnable()
        {
            base.OnEnable();

            _enemyFollowController = GetComponent<EnemyFollowController>();
            base.SoundController = GetComponent<SoundController>();
            
            _isAttacking = false;
            _enemyFollowController.ChasePlayer = true;
        }

        private void Start()
        {
            foreach (ParticleSystem particleSystem in
                     _preExplosionVisualEffects.GetComponentsInChildren<ParticleSystem>())
            {
                ParticleSystem.MainModule main = particleSystem.main;
                main.duration = this.EnemyStats.AttackCooldown;
                main.startSize =
                    3 * this.EnemyStats
                        .AttackRange; // TODO: Check if the range of the animation is the same as the attack
            }

            foreach (ParticleSystem particleSystem in _postExplosionVisualEffects
                         .GetComponentsInChildren<ParticleSystem>())
            {
                ParticleSystem.MainModule main = particleSystem.main;
                main.duration = 1;
                main.startSize =
                    5 * this.EnemyStats
                        .AttackRange; // TODO: Check if the range of the animation is the same as the attack
            }
        }

        void Update()
        {
            // if enemy is attacking, do nothing as it's exploding
            if (_isAttacking)
            {
                _enemyFollowController.ChasePlayer = false;
                return;
            }

            float distanceFromPlayer = _enemyFollowController.getDistanceFromPlayer();
            if (distanceFromPlayer < 5f) // TODO: HARDCODED
                Attack();
        }

        public override void Attack()
        {
            // add a cooldown to give the player time to react
            if (_attackCooldown.IsOnCooldown()) return;

            GetComponent<Animator>().SetTrigger("Sleep");
            _isAttacking = true;

            foreach (ParticleSystem particleSystem in
                     _preExplosionVisualEffects.GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Play();
            }
            
            new CmdAttackSound(SoundController).Execute();

            _preExplosionCoroutine = StartCoroutine(_attackCooldown.CallbackCooldown(this.Stats.AttackCooldown, Explode));
        }

        private void Explode()
        {
            foreach (ParticleSystem particleSystem in
                     _postExplosionVisualEffects.GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Play();
            }

            // get body component
            // GameObject.Find("Body").SetActive(false);
            _postExplosionCoroutine = StartCoroutine(new Cooldown().CallbackCooldown(2f,
                AfterExplosion)); // last little bit longer to wait for animation to finish
        }

        private void AfterExplosion()
        {
            // detect if the player is in range
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, this.EnemyStats.AttackRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Player"))
                {
                    bool hitCrit = Random.Range(0, 1) < this.Stats.CritProbability;
                    
                    // get the distance within the sphere and multiply it by the damage
                    float distance = _enemyFollowController.getDistanceFromPlayer();
                    if (distance > this.EnemyStats.AttackRange) distance = this.EnemyStats.AttackRange;
                    
                    // when enemy is touching the player, it does xAttackRange damage
                    // if it's further away, it does less damage
                    // formula = r - d * (1 - 1 / r) where r is the radius and d the distance
                    float damageMultiplier = this.EnemyStats.AttackRange - distance * (1 - (1 / this.EnemyStats.AttackRange));
                    hitCollider.gameObject.GetComponent<LifeController>().TakeDamage(this.Stats.Damage * damageMultiplier, hitCrit);
                    break;
                }
            }

            Die(Killer.ENEMY);
        }

        public override void OnDisable()
        {
            _attackCooldown.Reset();
            if(_preExplosionCoroutine != null) StopCoroutine(_preExplosionCoroutine);
            if(_postExplosionCoroutine != null) StopCoroutine(_postExplosionCoroutine);

            base.OnDisable();
        }
    }
}