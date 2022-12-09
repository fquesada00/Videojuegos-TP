using System;
using Controllers;
using Controllers.NavMesh;
using Controllers.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    [RequireComponent(typeof(LifeController))]
    public class DevilBulldogEnemy : Enemy
    {
        private Cooldown _attackCooldown;
        private Cooldown _sniffCooldown;
        private Animator _animator;
        private Actor _player;

        new void OnEnable()
        {
            base.OnEnable();

            _attackCooldown = new Cooldown();
            _sniffCooldown = new Cooldown();

            _enemyFollowController = GetComponent<EnemyFollowController>();
            _animator = GetComponent<Animator>();
            base.SoundController = GetComponent<SoundController>();
        }

        private void Start()
        {
            _player = _enemyFollowController.Player;
            _enemyFollowController.ChasePlayer = false;
        }

        void Update()
        {
            _enemyFollowController.LookAtPlayer();
            
            float distanceFromPlayer = _enemyFollowController.getDistanceFromPlayer();
            if (distanceFromPlayer < EnemyStats.AttackRange)
            {
                Attack();
            }
            else if (distanceFromPlayer < 50f && Random.Range(0, 100) < 50)
            {
                Sniff();
            }
        }

        public override void Attack()
        {
            if (_attackCooldown.IsOnCooldown()) return;
            _animator.SetTrigger("roar");
            StartCoroutine(_attackCooldown.BooleanCooldown(Stats.AttackCooldown));
        }

        private void Sniff()
        {
            if (_sniffCooldown.IsOnCooldown()) return;
            _animator.SetTrigger("sniff");
            StartCoroutine(_sniffCooldown.BooleanCooldown(2f));
        }
    }
}