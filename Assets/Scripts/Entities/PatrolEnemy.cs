using System;
using Controllers.NavMesh;
using UnityEngine;
using UnityEngine.AI;
using FlyWeights.EntitiesStats;

namespace Entities
{
    public abstract class PatrolEnemy : Enemy
    {

        public PatrolStats PatrolStats => _patrolStats;
        [SerializeField] private PatrolStats _patrolStats;
        public Vector3 _wanderTarget;
        private float _speed;
        
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        protected Actor _player;
        private NavMeshAgent _navMeshAgent;
        

        protected override void OnEnable()
        {
            _player = FindObjectOfType<Actor>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _wanderTarget = GetRandomGameBoardLocation();
            _speed = this.PatrolStats.Speed;
        }

        private void Patrol()
        {
            if (Vector3.Distance(transform.position, _wanderTarget) < this.PatrolStats.MinWanderTargetDistance)
            {
                _wanderTarget = GetRandomGameBoardLocation();
            }
                
            transform.position = Vector3.MoveTowards(
                transform.position,
                _wanderTarget,
                _speed * Time.deltaTime);
            transform.LookAt(_wanderTarget);
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

        private Vector3 GetRandomGameBoardLocation()
        {
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

            int maxIndices = navMeshData.indices.Length - 3;

            // pick the first indice of a random triangle in the nav mesh
            int firstVertexSelected = UnityEngine.Random.Range(0, maxIndices);
            int secondVertexSelected = UnityEngine.Random.Range(0, maxIndices);

            // spawn on verticies
            Vector3 point = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];

            Vector3 firstVertexPosition = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
            Vector3 secondVertexPosition = navMeshData.vertices[navMeshData.indices[secondVertexSelected]];

            // eliminate points that share a similar X or Z position to stop spawining in square grid line formations
            if ((int)firstVertexPosition.x == (int)secondVertexPosition.x ||
                (int)firstVertexPosition.z == (int)secondVertexPosition.z)
            {
                point =
                    GetRandomGameBoardLocation(); // re-roll a position - I'm not happy with this recursion it could be better
            }
            else
            {
                // select a random point on it
                point = Vector3.Lerp(firstVertexPosition, secondVertexPosition, UnityEngine.Random.Range(0.05f, 0.95f));
            }

            return point;
        }
    }
}