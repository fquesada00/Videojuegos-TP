using System.Collections;
using System.Collections.Generic;
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
        protected Actor _player;
        protected override void Awake() {
            base.Awake();
            _player = FindObjectOfType<Actor>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Vector3 newPos = EnemySpawnManager.GetRandomNearbyPosition(_player.transform.position, _patrolStats.MinWanderTargetDistance);
            Debug.Log("New position: " + newPos);
            _wanderTarget = GetFlightPosition(newPos);
            _speed = this.PatrolStats.Speed;
        }

        private void Patrol()
        {
            //Distance ignoring Y axis less than 1

            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(_wanderTarget.x, 0, _wanderTarget.z)) < 1)
            {
                Vector3 newPos = EnemySpawnManager.GetRandomNearbyPosition(this.transform.position, _patrolStats.MinWanderTargetDistance);
                _wanderTarget = GetFlightPosition(newPos);
                Debug.Log("New position: " + newPos);
            }
            this.transform.position = Vector3.MoveTowards(transform.position, _wanderTarget, _speed * Time.deltaTime);
            //Rotate gradually ignoring Y axis
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(_wanderTarget.x, 0, _wanderTarget.z) - new Vector3(transform.position.x, 0, transform.position.z)), 1);
        }


        protected void Update()
        {
            // if enemy is near the player, chase the player
            float distanceFromPlayer = GetDistanceFromPlayer();
            if (distanceFromPlayer < this.PatrolStats.MaxTargetDistance)
            {
                // Set wander target 1 unit in front of the player in the xz plane
                _wanderTarget = GetFlightPosition(_player.transform.position + _player.transform.forward * 5);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(_wanderTarget.x, 0, _wanderTarget.z) - new Vector3(transform.position.x, 0, transform.position.z)), 1);
                transform.position = Vector3.MoveTowards(transform.position, _wanderTarget, _speed * Time.deltaTime);
                Debug.Log("Chasing player");
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
            // Distance ignoring Y axis
            return Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(_player.transform.position.x, 0, _player.transform.position.z));
        }
        
        public bool IsOnEnemyRange()
        {
            return GetDistanceFromPlayer() < this.EnemyStats.AttackRange;
        }

        private Vector3 GetFlightPosition(Vector3 pos)
        {
            //Ray cast to ground Layer
            RaycastHit hit;
            if (Physics.Raycast(pos, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Debug.DrawRay(pos, Vector3.down * hit.distance, Color.red);
                return new Vector3(pos.x, hit.point.y + _flightHeight, pos.z);
            }
            return pos;
        }
    }
}