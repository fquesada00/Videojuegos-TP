using System;
using Controllers;
using Controllers.NavMesh;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(LifeController), typeof(GameObject), typeof(EnemyFollowController))]
    public class DragonBoarEnemy : Boss
    {
        private Vector3 _initialPosition;

        private void Start()
        {
            _enemyFollowController = GetComponent<EnemyFollowController>();
            _animator = GetComponent<Animator>();
            SoundController = GetComponent<SoundController>();
            _initialPosition = transform.position;
            _enemyFollowController.ChasePlayer = true;
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}