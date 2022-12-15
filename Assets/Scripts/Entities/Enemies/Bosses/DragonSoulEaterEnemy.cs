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

        private static float _maxChaseDistance = 70f;
        private static float _minChaseDistance = 20f;

        private Cooldown _fireballCooldown;
        private Cooldown _biteCooldown;
        private DragonSoulEaterEnemyState _state;
        private Vector3 _initialPosition;

        private void Start()
        {
            _enemyFollowController = GetComponent<EnemyFollowController>();
            _animator = GetComponent<Animator>();
            SoundController = GetComponent<SoundController>();
            _initialPosition = transform.position;
            _fireballCooldown = new Cooldown();
            _biteCooldown = new Cooldown();
            _state = DragonSoulEaterEnemyState.SLEEP;
            _enemyFollowController.ChasePlayer = false;
        }

        private void Update()
        {
            float distanceFromPlayer = _enemyFollowController.GetDistanceFromPlayer();
            switch (_state)
            {
                case DragonSoulEaterEnemyState.RETURN:
                    if (Vector3.Distance(transform.position, _initialPosition) < 5f)
                    {
                        _enemyFollowController.ChaseDestination = false;
                        Sleep();
                    } else if (distanceFromPlayer < _maxChaseDistance) {
                        _enemyFollowController.ChaseDestination = false;
                        _enemyFollowController.ChasePlayer = true;
                        _state = DragonSoulEaterEnemyState.CHASE;
                    }
                    break;

                case DragonSoulEaterEnemyState.SLEEP:
                    if (distanceFromPlayer < _maxChaseDistance) {
                        SleepToChase();
                    }
                    break;

                case DragonSoulEaterEnemyState.CHASE:
                    if(distanceFromPlayer < _minChaseDistance){
                        StopChasing();
                    }
                    else if (distanceFromPlayer > _maxChaseDistance)
                    {
                        GoToInitialPosition();
                        return;
                    }
                    else if (distanceFromPlayer < EnemyStats.AttackRange && !_fireballCooldown.IsOnCooldown())
                    {
                        StartCoroutine(_fireballCooldown.BooleanCooldown(EnemyStats.AttackCooldown));
                        FireballAttack();
                    }

                    break;

                case DragonSoulEaterEnemyState.CLOSE_ATTACK:
                    if (distanceFromPlayer >= _minChaseDistance)
                    {
                        AttackToChase();
                    } else if (!_biteCooldown.IsOnCooldown()) {
                        StartCoroutine(_biteCooldown.BooleanCooldown(1.5f));
                        Attack();
                    }
                    break;
            }
        }

        public void FireballAttack()
        {
            // throw fireball and go to chase
            _enemyFollowController.ChasePlayer = false;
            Animate(FireballTrigger);
            StartCoroutine(new Cooldown().CallbackCooldown(4f, () =>
            {
                AttackToChase();
            }));
        }

        public override void Attack()
        {
            Animate(BiteTrigger);
        }

        private void Animate(int trigger) => _animator.SetTrigger(trigger);

        private void Sleep()
        {
            _state = DragonSoulEaterEnemyState.SLEEP;
            Animate(IdleTrigger);
            Animate(SleepTrigger);
        }

        private void AttackToChase()
        {
            _state = DragonSoulEaterEnemyState.CHASE;
            Animate(WalkTrigger);
            _enemyFollowController.ChasePlayer = true;
        }

        private void SleepToChase()
        {
            _state = DragonSoulEaterEnemyState.CHASE;

            Animate(ScreamTrigger);
            Animate(WalkTrigger);

            StartCoroutine(new Cooldown().CallbackCooldown(2f, () =>
            {
                _enemyFollowController.ChasePlayer = true;
            }));
        }

        private void StopChasing() {
            _enemyFollowController.ChasePlayer = false;
            Animate(IdleTrigger);
            _state = DragonSoulEaterEnemyState.CLOSE_ATTACK;
        }
        
        private void GoToInitialPosition()
        {
            Debug.Log("Going to initial position");
            _enemyFollowController.ChasePlayer = false;
            _enemyFollowController.ChaseDestination = true;
            _enemyFollowController.ChangeDestination(_initialPosition);
            _state = DragonSoulEaterEnemyState.RETURN;
        }

        private enum DragonSoulEaterEnemyState
        {
            CLOSE_ATTACK,
            CHASE,
            SLEEP,
            RETURN
        }
    }
}