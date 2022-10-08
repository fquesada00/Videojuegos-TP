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
        private static readonly int Jumping = Animator.StringToHash("isJumping");
        private static readonly int Dashing = Animator.StringToHash("isDashing");
        private static readonly int Running = Animator.StringToHash("isRunning");
        private static readonly int Attacking = Animator.StringToHash("isAttacking");
        
        public int MaxContinuosJumps => GetComponentInParent<Actor>().ActorStats.MaxContinuosJumps;
        private int _currentContinuosJumps = 0;


        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void StopAllAnimations() {
            Idle();
            Rebind();
        }

        public void Rebind() {
            _animator.Rebind();
        }

        public void StartJump()
        {
            if (_currentContinuosJumps < MaxContinuosJumps)
            {
                _isJumping = true;
                _animator.SetBool(Jumping, true);
                _currentContinuosJumps++;
                _isIdle = false;
            }
        }

        public void StartDash()
        {
            _isDashing = true;
            _animator.SetBool(Dashing, true);
            _isIdle = false;
        }

        public void StartRun()
        {
            _isRunning = true;
            _animator.SetBool(Running, true);
            _isIdle = false;
        }

        public void Idle()
        {
            _isIdle = true;
            _animator.SetBool(Running, false);
            _isRunning = false;
            _animator.SetBool(Jumping, false);
            _isJumping = false;
            _animator.SetBool(Dashing, false);
            _isDashing = false;
            _animator.SetBool(Attacking, false);
            _isAttacking = false;
        }
        
        public void StartAttack()
        {
            _isAttacking = true;
            _animator.SetBool(Attacking, true);
            _isIdle = false;
        }

        public void StopJump()
        {
            _isJumping = false;
            _animator.SetBool(Jumping, false);
            _isIdle = true;
            _currentContinuosJumps = 0;
        }

        public void StopDash()
        {
            _isDashing = false;
            _animator.SetBool(Dashing, false);
            _isIdle = true;
        }

        public void StopRun()
        {
            _isRunning = false;
            _animator.SetBool(Running, false);
            _isIdle = true;
        }

        public void StopAttack()
        {
            _isAttacking = false;
            _animator.SetBool(Attacking, false);
            _isIdle = true;
        }

        private void StopIdle()
        {
            _isIdle = false;
        }
    }
}