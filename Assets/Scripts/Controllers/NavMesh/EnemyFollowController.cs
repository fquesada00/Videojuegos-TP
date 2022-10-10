using System;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers.NavMesh
{
    public class EnemyFollowController : MonoBehaviour
    {
        private Actor _player;
        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _player = FindObjectOfType<Actor>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _navMeshAgent.SetDestination(_player.transform.position);
            //look at player
            transform.LookAt(_player.transform);
        }

        public float getDistanceFromPlayer(){
            return Vector3.Distance(_player.transform.position, transform.position);
        }
    }
}