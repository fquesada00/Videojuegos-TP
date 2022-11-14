using Commands.Sounds;
using Controllers;
using Controllers.NavMesh;
using Controllers.Utils;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(LifeController), typeof(GameObject), typeof(GameObject))]
    public class CrabEnemy : Enemy
    {
        private Cooldown _attackCooldown = new Cooldown();
        private bool _isAttacking = false;
        [SerializeField] private GameObject _postExplosionVisualEffects;
        [SerializeField] private GameObject _preExplosionVisualEffects;

        private void Start()
        {
            foreach (ParticleSystem particleSystem in
                     _preExplosionVisualEffects.GetComponentsInChildren<ParticleSystem>())
            {
                ParticleSystem.MainModule main = particleSystem.main;
                main.duration = this.EnemyStats.AttackCooldown;
                main.startSize =
                    2 * this.EnemyStats
                        .AttackRange; // TODO: Check if the range of the animation is the same as the attack
            }

            foreach (ParticleSystem particleSystem in _postExplosionVisualEffects
                         .GetComponentsInChildren<ParticleSystem>())
            {
                ParticleSystem.MainModule main = particleSystem.main;
                main.duration = 1;
                main.startSize =
                    2 * this.EnemyStats
                        .AttackRange; // TODO: Check if the range of the animation is the same as the attack
            }
        }

        new void OnEnable()
        {
            base.OnEnable();

            _enemyFollowController = GetComponent<EnemyFollowController>();

            base.SoundController = GetComponent<SoundController>();
        }

        void Update()
        {
            // if enemy is attacking, do nothing as it's exploding
            if (_isAttacking)
            {
                _enemyFollowController.MoveToPlayer = false;
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

            StartCoroutine(_attackCooldown.CallbackCooldown(this.Stats.AttackCooldown, Explode));
        }

        private void Explode()
        {
            foreach (ParticleSystem particleSystem in
                     _postExplosionVisualEffects.GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Play();
            }

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

            // get body component
            GameObject.Find("Body").SetActive(false);
            StartCoroutine(new Cooldown().CallbackCooldown(2,
                Die)); // last little bit longer to wait for animation to finish
        }
    }
}