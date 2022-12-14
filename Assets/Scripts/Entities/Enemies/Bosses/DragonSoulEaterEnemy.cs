using System;
using Controllers;
using Controllers.NavMesh;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(LifeController), typeof(GameObject), typeof(EnemyFollowController))]
    public class DragonSoulEaterEnemy : Boss
    {
        private static readonly int WalkTrigger = Animator.StringToHash("walk");

        private void Start()
        {
            _enemyFollowController = GetComponent<EnemyFollowController>();
            _animator = GetComponent<Animator>();
            SoundController = GetComponent<SoundController>();
            
            _enemyFollowController.ChasePlayer = true;
            Animate(WalkTrigger);
        }

        private void Update()
        {
            
        }

        public override void Attack()
        {
        }
        
        private void Animate(int trigger) => _animator.SetTrigger(trigger);
        
    }
}