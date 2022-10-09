using System;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.NavMesh
{
    public class EnemyFollow : MonoBehaviour
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
        }
    }
}