using System;
using Controllers.NavMesh;
using UnityEngine;
using UnityEngine.AI;
using FlyWeights.EntitiesStats;
using Controllers;

namespace Entities
{
    public abstract class PatrolEnemy : NavMeshEnemy
    {
        public PatrolStats PatrolStats => _patrolStats;
        [SerializeField] private PatrolStats _patrolStats;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        private NavMeshAgent _navMeshAgent;

        private bool _chasing;

        protected override void OnEnable()
        {
            base.OnEnable();
            _player = FindObjectOfType<Actor>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = this.PatrolStats.Speed;    
            GetComponent<LifeController>().OnTakeDamage += OnTakeDamage;   
        }

        private void Patrol()
        {
            if (_navMeshAgent.remainingDistance < 6f)
            {
                Vector3 target = EnemySpawnManager.GetRandomPositionOnNavMesh(this.transform.position, _patrolStats.MinWanderTargetDistance);
                _navMeshAgent.SetDestination(target);
            }
        }

        protected void Update()
        {
            // if enemy is near the player, chase the player
            float distanceFromPlayer = GetDistanceFromPlayer();
            if (distanceFromPlayer < this.PatrolStats.MaxTargetDistance)
            {
                _chasing = true;
                _navMeshAgent.SetDestination(EnemySpawnManager.GetNavMeshPosition(_player.transform.position));
            }
            else
            {
                _chasing = false;
                // if enemy is not near the player, wander around the game board
                Patrol();
            }
        }

        public abstract override void Attack();

        private float GetDistanceFromPlayer()
        {
            return Vector3.Distance(transform.position, _player.transform.position);
        }
        
        public bool IsOnEnemyRange()
        {
            return GetDistanceFromPlayer() < this.EnemyStats.AttackRange;
        }

         private void OnTakeDamage(float damage)
        {
            // Go To player only if not chasing
            if (_chasing) return;
            _navMeshAgent.SetDestination(EnemySpawnManager.GetNavMeshPosition(_player.transform.position));
        }
    }
}