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
        private IEnumerator _moveCoroutine;

        protected override void OnEnable()
        {
            base.OnEnable();
            _player = FindObjectOfType<Actor>();
            Vector3 newPos = EnemySpawnManager.GetRandomNearbyPosition(this.transform.position, _patrolStats.MinWanderTargetDistance);
            _wanderTarget = GetFlightPosition(newPos);
            _speed = this.PatrolStats.Speed;
            _moveCoroutine = StartCoroutine(MoveObject(transform, transform.position, _wanderTarget, _speed));
        }

        private void Patrol()
        {
            if (Vector3.Distance(transform.position, _wanderTarget) < 1)
            {
                StopCoroutine(_moveCoroutine);

                Vector3 newPos = EnemySpawnManager.GetRandomNearbyPosition(this.transform.position, _patrolStats.MinWanderTargetDistance);
                _wanderTarget = GetFlightPosition(newPos);
                // Move object to new position gradually
                _moveCoroutine = StartCoroutine(MoveObject(transform, transform.position, _wanderTarget, _speed));
            }
        }

        private IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
        {
            float i = 0.0f;
            float rate = 1.0f / time;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                //Rotate
                thisTransform.rotation = Quaternion.Lerp(thisTransform.rotation, Quaternion.LookRotation(endPos - thisTransform.position), i);
                thisTransform.position = Vector3.Lerp(startPos, endPos, i);
                yield return null;
            }
        }

        protected void Update()
        {
            // if enemy is near the player, chase the player
            float distanceFromPlayer = GetDistanceFromPlayer();
            if (distanceFromPlayer < this.PatrolStats.MaxTargetDistance)
            {
                transform.rotation = Quaternion.LookRotation(_player.transform.position - transform.position);
                transform.position = Vector3.MoveTowards(transform.position, _wanderTarget, _speed * Time.deltaTime);

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
            return Vector3.Distance(transform.position,GetFlightPosition(_player.transform.position));
        }
        
        public bool IsOnEnemyRange()
        {
            return GetDistanceFromPlayer() < this.EnemyStats.AttackRange;
        }

        private Vector3 GetFlightPosition(Vector3 pos)
        {
            //Ray cast to ground tag and calculate the desired height
            RaycastHit hit;
            if (Physics.Raycast(pos, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                return new Vector3(pos.x, hit.point.y + _flightHeight, pos.z);
            }
            return pos;
        }
    }
}