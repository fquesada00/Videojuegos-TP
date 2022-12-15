using Controllers;
using UnityEngine;
using FlyWeights.EntitiesStats;

namespace Entities
{
    [RequireComponent(typeof(PatrolStats))]
    public abstract class FlyingEnemy : Enemy
    {
        public PatrolStats PatrolStats => _patrolStats;
        [SerializeField] private PatrolStats _patrolStats;
        public float FlightHeight => _flightHeight;
        [SerializeField] private float _flightHeight;
        private float _speed;
        public Vector3 _wanderTarget;
        private LifeController _lifeController;

        private bool _chasing;
        protected override void Awake()
        {
            base.Awake();
            _lifeController = GetComponent<LifeController>();
            _lifeController.OnTakeDamage += OnTakeDamage;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Vector3 newPos = EnemySpawnManager.GetRandomNearbyPositionXZ(_player.transform.position, _patrolStats.MinWanderTargetDistance);
            transform.position = GetFlightPosition(newPos);
            _wanderTarget = GetFlightPosition(_player.transform.position);
            _speed = this.PatrolStats.Speed;
            _chasing = false;
        }

        private void Patrol()
        {
            //Distance ignoring Y axis less than 1

            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(_wanderTarget.x, 0, _wanderTarget.z)) < 1)
            {
                Vector3 newPos = EnemySpawnManager.GetRandomNearbyPositionXZ(this.transform.position, _patrolStats.MinWanderTargetDistance);
                _wanderTarget = GetFlightPosition(newPos);
            }
            this.transform.position = GetFlightPosition(Vector3.MoveTowards(transform.position, _wanderTarget, _speed * Time.deltaTime));
            //Rotate gradually ignoring Y axis
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(_wanderTarget.x, 0, _wanderTarget.z) - new Vector3(transform.position.x, 0, transform.position.z)), 1);
        }


        protected void Update()
        {
            // if enemy is near the player, chase the player
            float distanceFromPlayer = GetDistanceFromPlayer();
            if (distanceFromPlayer < this.PatrolStats.MaxTargetDistance)
            {
                _chasing = true;
                // Set wander target 5 units within player radius in the shortest direction
                Vector3 direction = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(_player.transform.position.x, 0, _player.transform.position.x);
                Vector3 normalizedDirection = direction.normalized;
                _wanderTarget = GetFlightPosition(_player.transform.position + normalizedDirection * 7);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(_wanderTarget.x, 0, _wanderTarget.z) - new Vector3(transform.position.x, 0, transform.position.z)), 1);
                transform.position = GetFlightPosition(Vector3.MoveTowards(transform.position, _wanderTarget, _speed * Time.deltaTime));
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
            // Distance ignoring Y axis
            return Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(_player.transform.position.x, 0, _player.transform.position.z));
        }

        public bool IsOnEnemyRange()
        {
            return GetDistanceFromPlayer() < this.EnemyStats.AttackRange;
        }

        private Vector3 GetFlightPosition(Vector3 pos)
        {

            float height = Terrain.activeTerrain.SampleHeight(pos);
            return new Vector3(pos.x, height + _flightHeight, pos.z);
        }

        private void OnTakeDamage(float damage)
        {
            // Go To player only if not chasing
            if (_chasing) return;
            _wanderTarget = GetFlightPosition(_player.transform.position);
        }
    }
}