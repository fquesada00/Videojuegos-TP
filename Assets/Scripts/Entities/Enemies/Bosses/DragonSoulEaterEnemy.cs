using System;
using Controllers;
using Controllers.NavMesh;
using Controllers.Utils;
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

        private static float _chaseDistance = 50f;
        private static float _minChaseDistance = 30f;

        private Cooldown _attackCooldown;

        private DragonSoulEaterEnemyState _state = DragonSoulEaterEnemyState.SLEEP;
        private Vector3 _initialPosition;

        private void Start()
        {
            _enemyFollowController = GetComponent<EnemyFollowController>();
            _animator = GetComponent<Animator>();
            SoundController = GetComponent<SoundController>();
            _initialPosition = transform.position;
            _enemyFollowController.ChasePlayer = false;
            _attackCooldown = new Cooldown();
            Animate(SleepTrigger);
        }

        private void Update()
        {
            float distanceFromPlayer = _enemyFollowController.getDistanceFromPlayer();
            if (distanceFromPlayer > _chaseDistance)
            {
                if (Vector3.Distance(transform.position, _initialPosition) < 1)
                {
                    GoToInitialPosition();
                }
                else
                {
                    GoToSleep();
                }
            }
            else if (distanceFromPlayer > EnemyStats.AttackRange)
            {
                Chase();
            }
            else
            {
                Attack();
            }
            // switch (_state)
            // {
            //     case DragonSoulEaterEnemyState.SLEEP:
            //         if (distanceFromPlayer < _chaseDistance)
            //         {
            //             Chase();
            //         }
            //
            //         break;
            //     case DragonSoulEaterEnemyState.CHASE:
            //         if (distanceFromPlayer > _chaseDistance)
            //         {
            //             GoToSleep();
            //         }
            //         else if (distanceFromPlayer < EnemyStats.AttackRange)
            //         {
            //             Attack();
            //         }
            //
            //         break;
            //     case DragonSoulEaterEnemyState.ATTACK:
            //         if (distanceFromPlayer > _chaseDistance)
            //         {
            //             GoToSleep();
            //         }
            //
            //         if (distanceFromPlayer > EnemyStats.AttackRange)
            //         {
            //             Chase();
            //         }
            //         else
            //         {
            //             Attack();
            //         }
            //
            //         break;
            // }
        }

        public override void Attack()
        {
            if (_attackCooldown.IsOnCooldown()) return;

                StopChasing();
                // StartCoroutine(new Cooldown().BooleanCooldown(1f));
            Debug.Log("Attacking");
            _state = DragonSoulEaterEnemyState.ATTACK;
            Animate(FireballTrigger);
            StartCoroutine(_attackCooldown.BooleanCooldown(EnemyStats.AttackCooldown));
        }

        private void Animate(int trigger) => _animator.SetTrigger(trigger);

        private void GoToSleep()
        {
            if (_state == DragonSoulEaterEnemyState.SLEEP) return;
            StopChasing();
            _state = DragonSoulEaterEnemyState.SLEEP;
            Animate(IdleTrigger);
            Animate(SleepTrigger);
        }

        private void Chase()
        {
            if (_enemyFollowController.getDistanceFromPlayer() < _minChaseDistance)
            {
                StopChasing();
                return;
            }

            if (_state == DragonSoulEaterEnemyState.CHASE) return;
            _state = DragonSoulEaterEnemyState.CHASE;

            Animate(ScreamTrigger);
            Animate(WalkTrigger);

            StartCoroutine(new Cooldown().CallbackCooldown(2f, () =>
            {
                _enemyFollowController.ChasePlayer = true;
            }));
        }

        private void StopChasing() =>
            _enemyFollowController.ChasePlayer = false;
        
        private void GoToInitialPosition()
        {
            _enemyFollowController.ChasePlayer = false;
            _enemyFollowController.ChangeDestination(_initialPosition);
            Animate(WalkTrigger);
        }

        private enum DragonSoulEaterEnemyState
        {
            ATTACK,
            CHASE,
            SLEEP
        }
    }
}