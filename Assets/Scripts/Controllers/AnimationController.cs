using System;
using Strategies;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour, IAnimatable
    {
        private bool _isRunning = false;
        public bool IsRunning => _isRunning;
        
        private bool _isJumping = false;
        public bool IsJumping => _isJumping;
        
        private bool _isDashing = false;
        public bool IsDashing => _isDashing;
        
        private bool _isAttacking = false;
        public bool IsAttacking => _isAttacking;

        private bool _isIdle = true;
        public bool IsIdle => _isIdle;


        private Animator _animator;
        private static readonly int GroundedId = Animator.StringToHash("Grounded");
        private static readonly int RunningId = Animator.StringToHash("isRunning");
        private static readonly int AttackId = Animator.StringToHash("attack");
        private static readonly int SpeedId = Animator.StringToHash("speed");
        
        public int MaxContinuosJumps => GetComponentInParent<Actor>().ActorStats.MaxContinuosJumps;


        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
 



        public void setGrounded(bool grounded)
        {
            _animator.SetBool(GroundedId, grounded);
        }

        
        public void Attack()
        {
            _animator.SetTrigger(AttackId);
        }  
        
    

        public void Run(float speed)
        {
            _animator.SetFloat(SpeedId, speed);
        }

        public void SetWeapon(int index)
        {
            _animator.SetInteger("weapon", index);
        }
    }
}