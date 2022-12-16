using System;
using Controllers;
using Controllers.NavMesh;
using Controllers.Utils;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(LifeController), typeof(GameObject), typeof(EnemyFollowController))]
    public class DragonBoarEnemy : Boss
    {
        private static readonly int IdleTrigger = Animator.StringToHash("Idle");
        private static readonly int ScreamTrigger = Animator.StringToHash("Scream");
        private static readonly int WalkTrigger = Animator.StringToHash("Walk");

        private Vector3 _initialPosition;
        private DragonBoarState _state;
        private float _maxChaseDistance = 30f;
        private float _minChaseDistance = 10f;

        private void Start()
        {
            _enemyFollowController = GetComponent<EnemyFollowController>();
            _animator = GetComponent<Animator>();
            SoundController = GetComponent<SoundController>();
            _initialPosition = transform.position;
            _enemyFollowController.ChasePlayer = false;
            _state = DragonBoarState.IDLING;
        }

        private void Update()
        {
            float distanceFromPlayer = _enemyFollowController.GetDistanceFromPlayer();
            _state = NextState(_state, distanceFromPlayer);
        }

        private DragonBoarState NextState(DragonBoarState state, float distanceFromPlayer)
        {
            switch (state)
            {
                case DragonBoarState.IDLING:
                    if (distanceFromPlayer <= _maxChaseDistance)
                    {
                        ChasePlayer();
                        return DragonBoarState.CHASING;
                    }
                    break;
                case DragonBoarState.CHASING:
                    if (distanceFromPlayer > _maxChaseDistance)
                    {
                        ReturnToInitialPosition();
                        return DragonBoarState.RETURNING_TO_INITIAL_POSITION;
                    }
                    break;
                case DragonBoarState.SCREAMING:
                    break;
                case DragonBoarState.RETURNING_TO_INITIAL_POSITION:
                    if (distanceFromPlayer <= _maxChaseDistance)
                    {
                        ChasePlayer();
                        return DragonBoarState.CHASING;
                    }

                    if(Vector3.Distance(transform.position, _initialPosition) <= 5f)
                    {
                        Idle();
                        return DragonBoarState.IDLING;
                    }
                    break;
            }

            return state;
        }

        private void Animate(int trigger) => _animator.SetTrigger(trigger);

        private void ChasePlayer()
        {
            if (_state != DragonBoarState.RETURNING_TO_INITIAL_POSITION) Animate(WalkTrigger);
            StartCoroutine(new Cooldown().CallbackCooldown(1f, () => _enemyFollowController.ChasePlayer = true));
        }
        
        private void ReturnToInitialPosition()
        {
            if (_state != DragonBoarState.CHASING) Animate(WalkTrigger);
            StartCoroutine(new Cooldown().CallbackCooldown(1f, () =>
            {
                _enemyFollowController.ChasePlayer = false;
                _enemyFollowController.ChaseDestination = true;
                _enemyFollowController.ChangeDestination(_initialPosition);
            }));
        }

        private void Idle()
        {
            Animate(IdleTrigger);
            _enemyFollowController.ChasePlayer = false;
            _enemyFollowController.ChaseDestination = false;
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }

    public enum DragonBoarState
    {
        IDLING,
        SCREAMING,
        CHASING,
        RETURNING_TO_INITIAL_POSITION
    }
}