using System;
using Controllers;
using Controllers.NavMesh;
using Utils;
using Strategies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    [RequireComponent(typeof(LifeController), typeof(GameObject), typeof(EnemyFollowController))]
    public class DevilBulldogEnemy : Boss
    {
        private Cooldown _attackCooldown;
        private Cooldown _sniffCooldown;
        private Actor _player;
        [SerializeField] private GameObject _poisonSpellPrefab;
        private static readonly int RoarTrigger = Animator.StringToHash("roar");
        private static readonly int SniffTrigger = Animator.StringToHash("sniff");

        private void Start()
        {
            _attackCooldown = new Cooldown();
            _sniffCooldown = new Cooldown();

            _enemyFollowController = GetComponent<EnemyFollowController>();
            _animator = GetComponent<Animator>();
            SoundController = GetComponent<SoundController>();
            
            _enemyFollowController.ChasePlayer = false;
        }

        void Update()
        {
            float distanceFromPlayer = _enemyFollowController.GetDistanceFromPlayer();
            if (distanceFromPlayer < EnemyStats.AttackRange)
            {
                Attack();
            }
            else if (distanceFromPlayer < (EnemyStats.AttackRange * 2) && Random.Range(0, 100) < 50)
            {
                Sniff();
            }
        }

        public override void Attack()
        {
            if (_attackCooldown.IsOnCooldown()) return;
            _animator.SetTrigger(RoarTrigger);
            StartCoroutine(_attackCooldown.BooleanCooldown(Stats.AttackCooldown));
            StartCoroutine(new Cooldown().CallbackCooldown(2f, CastPoisonSpell));
        }

        private void CastPoisonSpell()
        {
            Vector3 playerPosition = _player.transform.position;
            var poisonSpell = Instantiate(_poisonSpellPrefab, playerPosition, Quaternion.identity);
            ISpell IPoisonSpell = poisonSpell.GetComponent<ISpell>();
            IPoisonSpell.Damage = Stats.Damage;
            IPoisonSpell.Duration = 10;
            IPoisonSpell.Crit = true;
            IPoisonSpell.Range = 10;
        }

        private void Sniff()
        {
            if (_sniffCooldown.IsOnCooldown()) return;
            _animator.SetTrigger(SniffTrigger);
            StartCoroutine(_sniffCooldown.BooleanCooldown(2f));
        }
    }
}