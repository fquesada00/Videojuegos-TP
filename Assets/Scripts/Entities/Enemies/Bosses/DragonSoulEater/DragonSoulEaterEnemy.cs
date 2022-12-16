using Commands.Sounds;
using Controllers;
using Controllers.NavMesh;
using Utils;
using UnityEngine;
using FlyWeights.EntitiesStats;

namespace Entities
{
    [RequireComponent(typeof(LifeController), typeof(GameObject), typeof(EnemyFollowController))]
    public class DragonSoulEaterEnemy : Boss
    {
        private static readonly int IdleTrigger = Animator.StringToHash("idle");
        private static readonly int WalkTrigger = Animator.StringToHash("walk");
        private static readonly int SleepTrigger = Animator.StringToHash("sleep");
        private static readonly int ScreamTrigger = Animator.StringToHash("scream");
        private static readonly int BasicAttackTrigger = Animator.StringToHash("basic_attack");
        private static readonly int FireballTrigger = Animator.StringToHash("fireball");
        private static readonly int DieTrigger = Animator.StringToHash("die");

        private static float _maxChaseDistance = 90f;
        private static float _minChaseDistance = 30f;

        [SerializeField] private CannonballThrower _canonballThrower;
        [SerializeField] private GameObject _landAttackParticleSystems;
        [SerializeField] private ExplosionStats _landAttackExplosionStats;

        private Cooldown _fireballCooldown;
        private Cooldown _basicAttackCooldown;
        private DragonSoulEaterEnemyState _state;
        private Vector3 _initialPosition;
        private bool _attacking = false;
        
        private CmdAttackSound _attackSound;

        private void Start()
        {
            _enemyFollowController = GetComponent<EnemyFollowController>();
            _animator = GetComponent<Animator>();
            SoundController = GetComponent<SoundController>();
            _initialPosition = transform.position;
            _fireballCooldown = new Cooldown();
            _basicAttackCooldown = new Cooldown();
            _state = DragonSoulEaterEnemyState.SLEEP;
            _enemyFollowController.ChasePlayer = false;
            _attackSound = new CmdAttackSound(SoundController);
        }

        private void Update()
        {
            if (_attacking) return;

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
                    }
                    else if (distanceFromPlayer < EnemyStats.AttackRange && !_fireballCooldown.IsOnCooldown())
                    {
                        StartCoroutine(_fireballCooldown.BooleanCooldown(EnemyStats.AttackCooldown));
                        CannonballAttackAnimation();
                    }

                    break;

                case DragonSoulEaterEnemyState.CLOSE_ATTACK:
                    if (distanceFromPlayer >= _minChaseDistance)
                    {
                        Debug.Log("AttackToChase");
                        AttackToChase();
                    } else if (!_basicAttackCooldown.IsOnCooldown()) {
                        StartCoroutine(_basicAttackCooldown.BooleanCooldown(10f));
                        Attack();
                    }
                    break;
                case DragonSoulEaterEnemyState.DIE:
                default:
                    return;
            }
        }

        public void ExecuteCannonballAttack() 
        {
            _canonballThrower.Target = _enemyFollowController.Player.gameObject.transform.position;
            StartCoroutine(new Cooldown().CallbackCooldown(.5f, () =>
            {
                _canonballThrower.Attack(false);
            }));

            StartCoroutine(new Cooldown().CallbackCooldown(4f, () =>
            {
                _enemyFollowController.ChasePlayer = true;
                _attacking = false;
            }));
        }

        private void CannonballAttackAnimation()
        {
            _attacking = true;
            // throw fireball and go to chase
            _enemyFollowController.ChasePlayer = false;
            Animate(FireballTrigger);
            _attackSound.Execute();
        }

        public override void Attack()
        {
            _attacking = true;
            Animate(BasicAttackTrigger);
        }

        public void ExecuteLandAttack()
        {
            Debug.Log("ExecuteLandAttack");
            foreach(ParticleSystem particleSystem in _landAttackParticleSystems.GetComponentsInChildren<ParticleSystem>())
                particleSystem.Play();

            ExplosionRaycast.Explode(transform.position, _landAttackExplosionStats.Range, _landAttackExplosionStats.Damage);
        
            _attacking = false;
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
            _enemyFollowController.ChasePlayer = true;
            Animate(WalkTrigger);
        }

        private void SleepToChase()
        {
            _state = DragonSoulEaterEnemyState.CHASE;

            Animate(ScreamTrigger);

            StartCoroutine(new Cooldown().CallbackCooldown(2f, () =>
            {
                _enemyFollowController.ChasePlayer = true;
            }));
        }

        private void StopChasing() {
            Debug.Log("StopChasing");
            _enemyFollowController.ChasePlayer = false;
            Animate(IdleTrigger);
            _state = DragonSoulEaterEnemyState.CLOSE_ATTACK;
        }
        
        private void GoToInitialPosition()
        {
            _enemyFollowController.ChasePlayer = false;
            _enemyFollowController.ChaseDestination = true;
            _enemyFollowController.ChangeDestination(_initialPosition);
            _state = DragonSoulEaterEnemyState.RETURN;
        }

        public override void Die(Killer killer = Killer.PLAYER)
        {
            if (_state == DragonSoulEaterEnemyState.DIE) return;
            
            this.StopAllCoroutines();

            _enemyFollowController.ChaseDestination = false;
            _enemyFollowController.ChasePlayer = false;
            _state = DragonSoulEaterEnemyState.DIE;
            Animate(DieTrigger);
            
            StartCoroutine(new Cooldown().CallbackCooldown(5f, () =>
            {
                base.Die(killer);
            }));
        }

        private enum DragonSoulEaterEnemyState
        {
            CLOSE_ATTACK,
            CHASE,
            SLEEP,
            RETURN,
            DIE
        }
    }
}