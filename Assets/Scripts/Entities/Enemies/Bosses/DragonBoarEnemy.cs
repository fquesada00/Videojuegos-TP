using System;
using Commands.Sounds;
using Controllers;
using Controllers.NavMesh;
using Managers;
using Strategies;
using UnityEngine;
using Utils;

namespace Entities
{
    [RequireComponent(typeof(LifeController), typeof(GameObject), typeof(EnemyFollowController))]
    public class DragonBoarEnemy : Boss
    {
        [SerializeField] private GameObject _poisonSpellPrefab;

        private static readonly int IdleTrigger = Animator.StringToHash("Idle");
        private static readonly int ScreamTrigger = Animator.StringToHash("Scream");
        private static readonly int WalkTrigger = Animator.StringToHash("Walk");
        private static readonly int DieTrigger = Animator.StringToHash("Die");

        private Vector3 _initialPosition;
        private DragonBoarState _state;
        [SerializeField] private float _maxChaseDistance = 100f;
        [SerializeField] private float _minChaseDistance = 35f;
        private float _damageMultiplier;

        private Cooldown _screamCooldown;
        private Cooldown _screamAnimationCooldown;
        
        private CmdAttackSound _attackSound;
        private LifeController _lifeController;

        private void Start()
        {
            _enemyFollowController = GetComponent<EnemyFollowController>();
            _animator = GetComponent<Animator>();
            SoundController = GetComponent<SoundController>();
            _lifeController = GetComponent<LifeController>();
            _initialPosition = transform.position;
            _enemyFollowController.ChasePlayer = false;
            _state = DragonBoarState.IDLING;
            _screamCooldown = new Cooldown();
            _screamAnimationCooldown = new Cooldown();
            _attackSound = new CmdAttackSound(SoundController);
            _damageMultiplier = FindObjectOfType<GameManager>().GetCurrentDifficultyStats.EnemyDamageMultiplier;
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
                    if(!_lifeController.HasShield) _lifeController.AddShield();
                    if (distanceFromPlayer <= EnemyStats.AttackRange && !_screamCooldown.IsOnCooldown() && !_screamAnimationCooldown.IsOnCooldown())
                    {
                        if(_lifeController.HasShield) _lifeController.RemoveShield();
                        Attack();
                        return DragonBoarState.SCREAMING;
                    }
                    
                    if (distanceFromPlayer <= _maxChaseDistance && distanceFromPlayer > _minChaseDistance)
                    {
                        if(_lifeController.HasShield) _lifeController.RemoveShield();
                        ChasePlayer();
                        return DragonBoarState.CHASING;
                    }
                    break;
                case DragonBoarState.CHASING:
                    if(_lifeController.HasShield) _lifeController.RemoveShield();
                    if (distanceFromPlayer > _maxChaseDistance)
                    {
                        if(!_lifeController.HasShield) _lifeController.AddShield();
                        ReturnToInitialPosition();
                        return DragonBoarState.RETURNING_TO_INITIAL_POSITION;
                    }

                    if (distanceFromPlayer <= EnemyStats.AttackRange && !_screamCooldown.IsOnCooldown() && !_screamAnimationCooldown.IsOnCooldown())
                    {
                        if(_lifeController.HasShield) _lifeController.RemoveShield();
                        Attack();
                        return DragonBoarState.SCREAMING;
                    }
                    break;
                case DragonBoarState.SCREAMING:
                    if (_screamAnimationCooldown.IsOnCooldown())
                    {
                        return DragonBoarState.SCREAMING;
                    }
                    
                    if (distanceFromPlayer <= EnemyStats.AttackRange)
                    {
                        if (!_screamCooldown.IsOnCooldown())
                        {
                            Attack();
                            if (distanceFromPlayer > _minChaseDistance) return DragonBoarState.SCREAMING;
                            Idle();
                            return DragonBoarState.IDLING;
                        }

                        if (distanceFromPlayer <= _minChaseDistance)
                        {
                            Idle();
                            return DragonBoarState.IDLING;
                        }
                        
                        // ChasePlayer();
                        // return DragonBoarState.CHASING;
                    }
                    
                    if (distanceFromPlayer <= _maxChaseDistance && distanceFromPlayer > _minChaseDistance)
                    {
                        ChasePlayer();
                        return DragonBoarState.CHASING;
                    }

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
                case DragonBoarState.DYING:
                    // do nothing
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
            Animate(ScreamTrigger);
            StartCoroutine(new Cooldown().CallbackCooldown(1f, () => _attackSound.Execute()));
            StartCoroutine(_screamAnimationCooldown.BooleanCooldown(3f));
            _enemyFollowController.ChasePlayer = false;
            _enemyFollowController.ChaseDestination = false;
            StartCoroutine(_screamCooldown.BooleanCooldown(EnemyStats.AttackCooldown));
            StartCoroutine(new Cooldown().CallbackCooldown(2f, CastPoisonSpell));
        }
        
        private void CastPoisonSpell()
        {
            Vector3 playerPosition = _enemyFollowController.Player.transform.position;
            var poisonSpell = Instantiate(_poisonSpellPrefab, playerPosition, Quaternion.identity);
            ISpell IPoisonSpell = poisonSpell.GetComponent<ISpell>();
            IPoisonSpell.Damage = Stats.Damage * _damageMultiplier;
            IPoisonSpell.Duration = 10;
            IPoisonSpell.Range = 10;
        }

        public override void Die(Killer killer = Killer.PLAYER)
        {
            _state = DragonBoarState.DYING; // hacky
            _enemyFollowController.ChaseDestination = false;
            _enemyFollowController.ChasePlayer = false;
            Animate(DieTrigger);
            StartCoroutine(new Cooldown().CallbackCooldown(3f, () => base.Die(killer)));
        }
    }

    public enum DragonBoarState
    {
        IDLING,
        SCREAMING,
        CHASING,
        RETURNING_TO_INITIAL_POSITION,
        DYING
    }
}