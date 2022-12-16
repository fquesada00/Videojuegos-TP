using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

namespace Controllers.NavMesh
{
    public class EnemyFollowController : MonoBehaviour
    {
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        private Actor _player;
        private NavMeshAgent _navMeshAgent;
        
        private bool _chasePlayer;
        public bool ChasePlayer
        {
            set => _chasePlayer = value;
            get => _chasePlayer;
        }
        private bool _chaseDestination;
        public bool ChaseDestination
        {
            set => _chaseDestination = value;
            get => _chaseDestination;
        }

        private void Awake()
        {
            _player = FindObjectOfType<Actor>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _chasePlayer = true;
            _chaseDestination = false;
        }

        private void Update()
        {
            if (_chasePlayer) {
                _navMeshAgent.SetDestination(_player.transform.position);
            } else if(!_chaseDestination) {
                // not chasing player nor destination
                _navMeshAgent.velocity = Vector3.zero;
            }
        }
        
        public void ChangeDestination(Vector3 destination) => _navMeshAgent.SetDestination(destination);
        
        public float GetDistanceFromPlayer(){
            return Vector3.Distance(_player.transform.position, transform.position);
        }
        
        public Actor Player => _player;
    }
}