using System;
using Controllers;
using Controllers.NavMesh;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    [RequireComponent(typeof(LifeController), typeof(GameObject), typeof(EnemyFollowController))]
    public class DragonSoulEaterEnemy : Boss
    {
        private static readonly int IdleTrigger = Animator.StringToHash("idle");
        private static readonly int WalkTrigger = Animator.StringToHash("walk");
        private static readonly int SleepTrigger = Animator.StringToHash("sleep");
        private static readonly int ScreamTrigger = Animator.StringToHash("scream");
        private static readonly int BiteTrigger = Animator.StringToHash("bite");
        private static readonly int FireballTrigger = Animator.StringToHash("fireball");
        private static float _minBiteDistance = 20f;
        private DragonSoulEaterEnemyState _state = DragonSoulEaterEnemyState.Sleep;
        private Vector3 _initialPosition;

        private void Start()
        {
            _enemyFollowController = GetComponent<EnemyFollowController>();
            _animator = GetComponent<Animator>();
            SoundController = GetComponent<SoundController>();
            _initialPosition = transform.position;
            _enemyFollowController.ChasePlayer = false;
            Animate(SleepTrigger);
        }

        private void Update()
        {
            float distanceFromPlayer = _enemyFollowController.getDistanceFromPlayer();

            switch(_state){
                case DragonSoulEaterEnemyState.Sleep:
                    if(distanceFromPlayer < EnemyStats.AttackRange)
                    {
                        GoToChase();
                    }
                    break;
                
                case DragonSoulEaterEnemyState.Chase:
                    if(distanceFromPlayer > EnemyStats.AttackRange) {
                        GoToSleep();
                    } else if (distanceFromPlayer < _minBiteDistance || Random.Range(0, 100000000) < 5) // TODO: Cambiar range por cooldown del fireball?
                    {
                        Animate(IdleTrigger);
                        _enemyFollowController.ChasePlayer = false;
                        _state = DragonSoulEaterEnemyState.Attack;
                    }
                    break;
                case DragonSoulEaterEnemyState.Attack:
                    if(distanceFromPlayer > EnemyStats.AttackRange) {
                        GoToSleep();
                    } else {
                        Attack();
                    }
                    break;
            }
        }

        public override void Attack()
        {
            if(_enemyFollowController.getDistanceFromPlayer() > _minBiteDistance) {
                // Fireball
                Animate(FireballTrigger);
                // TODO: WAIT FOR ANIMATION TO FINISH?
                GoToChase();
            } else {
                // Bite
                Animate(BiteTrigger);
            }
        }
        
        private void Animate(int trigger) => _animator.SetTrigger(trigger);
        
        private void GoToSleep() {
            Debug.Log("Sleeping");
            _enemyFollowController.ChasePlayer = false;
            _state = DragonSoulEaterEnemyState.Sleep;
            Animate(IdleTrigger);
            Animate(SleepTrigger);
        }

        private void GoToChase() {
            Debug.Log("Chasing");
            _enemyFollowController.ChasePlayer = true;
            _state = DragonSoulEaterEnemyState.Chase;
            Animate(ScreamTrigger);
            // TODO: wait for animation to finish
            Animate(WalkTrigger);
        }

        private enum DragonSoulEaterEnemyState
        {
            Attack,
            Chase,
            Sleep
        }
    }
}