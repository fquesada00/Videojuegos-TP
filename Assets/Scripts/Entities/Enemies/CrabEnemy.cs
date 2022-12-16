using System;
using System.Collections.Generic;
using Commands.Sounds;
using Controllers;
using Controllers.NavMesh;
using Utils;
using UnityEngine;
using Random = UnityEngine.Random;
using FlyWeights.EntitiesStats;
using Managers;

namespace Entities
{
    [RequireComponent(typeof(LifeController), typeof(GameObject), typeof(GameObject))]
    public class CrabEnemy : NavMeshEnemy
    {
        [SerializeField] private ExplosionStats _explosionStats;
        [SerializeField] private GameObject _postExplosionVisualEffects;
        [SerializeField] private GameObject _preExplosionVisualEffects;
        private Cooldown _attackCooldown = new Cooldown();
        private bool _isAttacking = false;
        private float _damageMultiplier;
        
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
            _damageMultiplier = FindObjectOfType<GameManager>().GetCurrentDifficultyStats.EnemyDamageMultiplier;

            foreach (ParticleSystem particleSystem in
                     _preExplosionVisualEffects.GetComponentsInChildren<ParticleSystem>())
            {
                ParticleSystem.MainModule main = particleSystem.main;
                main.duration = this.EnemyStats.AttackCooldown;
                main.startSize =
                    (_explosionStats.Range/2) * this.EnemyStats
                        .AttackRange;
            }

            foreach (ParticleSystem particleSystem in _postExplosionVisualEffects
                         .GetComponentsInChildren<ParticleSystem>())
            {
                ParticleSystem.MainModule main = particleSystem.main;
                main.duration = 1;
                main.startSize =
                    _explosionStats.Range * this.EnemyStats
                        .AttackRange;
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

            float distanceFromPlayer = _enemyFollowController.GetDistanceFromPlayer();
            if (distanceFromPlayer < _explosionStats.Range)
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
            _postExplosionCoroutine = StartCoroutine(new Cooldown().CallbackCooldown(2f, AfterExplosion)); // last little bit longer to wait for animation to finish
        }

        private void AfterExplosion()
        {
            ExplosionRaycast.Explode(transform.position, this._explosionStats.Range, this._explosionStats.Damage * _damageMultiplier);
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