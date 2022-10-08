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

        private bool _isIdle = false;
        public bool IsIdle => _isIdle;

        private Animator _animator;
        private static readonly int Jumping = Animator.StringToHash("isJumping");
        private static readonly int Dashing = Animator.StringToHash("isDashing");
        private static readonly int Running = Animator.StringToHash("isRunning");
        private static readonly int Attacking = Animator.StringToHash("isAttacking");
        private static readonly int isIdle = Animator.StringToHash("isIdle");


        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void StartJump()
        {
            _isJumping = true;
            _animator.SetBool(Jumping, true);
        }

        public void StartDash()
        {
            _isDashing = true;
            _animator.SetBool(Dashing, true);
        }

        public void StartRun()
        {
            _isRunning = true;
            _animator.SetBool(Running, true);
        }

        public void Idle()
        {
            _isIdle = true;
            _animator.SetBool(isIdle, true);
            _animator.SetBool(Running, false);
            _animator.SetBool(Jumping, false);
            _animator.SetBool(Dashing, false);
            _animator.SetBool(Attacking, false);
        }
        
        public void StartAttack()
        {
            _isAttacking = true;
            _animator.SetBool(Attacking, true);
        }

        public void StopJump()
        {
            _isJumping = false;
            _animator.SetBool(Jumping, false);
        }

        public void StopDash()
        {
            _isDashing = false;
            _animator.SetBool(Dashing, false);
        }

        public void StopRun()
        {
            _isRunning = false;
            _animator.SetBool(Running, false);
        }

        public void StopAttack()
        {
            _isAttacking = false;
            _animator.SetBool(Attacking, false);
        }

        private void StopIdle()
        {
            _isIdle = false;
        }
    }
}