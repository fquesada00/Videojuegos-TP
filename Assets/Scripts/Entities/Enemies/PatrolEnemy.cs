using System;
using Controllers.NavMesh;
using UnityEngine;
using UnityEngine.AI;
using FlyWeights.EntitiesStats;

namespace Entities
{
    public abstract class PatrolEnemy : NavMeshEnemy
    {
        public PatrolStats PatrolStats => _patrolStats;
        [SerializeField] private PatrolStats _patrolStats;
        public Vector3 _wanderTarget;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        protected Actor _player;
        private NavMeshAgent _navMeshAgent;

        protected override void OnEnable()
        {
            base.OnEnable();
            _player = FindObjectOfType<Actor>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _wanderTarget = EnemySpawnManager.GetRandomPositionOnNavMesh(this.transform.position, _patrolStats.MinWanderTargetDistance);
            _navMeshAgent.speed = this.PatrolStats.Speed;       
        }

        private void Patrol()
        {
            if (Vector3.Distance(transform.position, _wanderTarget) < 1)
            {
                _wanderTarget = EnemySpawnManager.GetRandomPositionOnNavMesh(this.transform.position, _patrolStats.MinWanderTargetDistance);
                _navMeshAgent.SetDestination(_wanderTarget);
            }
        }

        protected void Update()
        {
            // if enemy is near the player, chase the player
            float distanceFromPlayer = GetDistanceFromPlayer();
            if (distanceFromPlayer < this.PatrolStats.MaxTargetDistance)
            {
                _navMeshAgent.SetDestination(_player.transform.position);
            }
            else
            {
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
    }
}