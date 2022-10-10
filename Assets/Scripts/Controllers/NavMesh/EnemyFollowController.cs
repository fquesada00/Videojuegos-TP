﻿using System;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers.NavMesh
{
    public class EnemyFollowController : MonoBehaviour
    {
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        private Actor _player;
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _player = FindObjectOfType<Actor>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _navMeshAgent.SetDestination(_player.transform.position);
            //look at player y axis
            transform.LookAt(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z));
            // transform.LookAt(_player.transform);
        }

        public float getDistanceFromPlayer(){
            return Vector3.Distance(_player.transform.position, transform.position);
        }
    }
}