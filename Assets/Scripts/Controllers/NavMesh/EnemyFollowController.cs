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

        private void Awake()
        {
            _player = FindObjectOfType<Actor>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _chasePlayer = true;
        }

        private void Update()
        {
            //look at player y axis
            LookAtPlayer();
            
            if (!_chasePlayer)
            {
                if (_navMeshAgent != null)
                {
                    _navMeshAgent.velocity = Vector3.zero;
                }
                return;
            }
            
            _navMeshAgent.SetDestination(_player.transform.position);

        }
        
        public void ChangeDestination(Vector3 destination) => _navMeshAgent.SetDestination(destination);
        

        public float getDistanceFromPlayer(){
            return Vector3.Distance(_player.transform.position, transform.position);
        }
        
        public Actor Player => _player;
        
        public void LookAtPlayer()
        {
            Vector3 playerPosition = _player.transform.position;
            transform.LookAt(new Vector3(playerPosition.x, transform.position.y, playerPosition.z));
        }
    }
}