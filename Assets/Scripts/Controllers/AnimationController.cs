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
        private static readonly int JumpId = Animator.StringToHash("jump");
        private static readonly int DashingId = Animator.StringToHash("isDashing");
        private static readonly int RunningId = Animator.StringToHash("isRunning");
        private static readonly int AttackId = Animator.StringToHash("attack");
        private static readonly int LandId = Animator.StringToHash("land");
        
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

        public void Jump()
        {
            if (_currentContinuosJumps < MaxContinuosJumps)
            {
                Rebind();
                _isJumping = true;
                _animator.SetTrigger(JumpId);
                _currentContinuosJumps++;
                _isIdle = false;
            }
        }

        public void StartDash()
        {
            _isDashing = true;
            _animator.SetBool(DashingId, true);
            _isIdle = false;
        }

        public void StartRun()
        {
            _isRunning = true;
            _animator.SetBool(RunningId, true);
            _isIdle = false;
        }

        public void Land()
        {
            _animator.SetTrigger(LandId);
            _currentContinuosJumps = 0;
            Idle();
        }

        public void Idle()
        {
            _isIdle = true;

            _animator.SetBool(RunningId, false);
            _isRunning = false;

            _isJumping = false;
            
            _animator.SetBool(DashingId, false);
            _isDashing = false;

            _isAttacking = false;
        }
        
        public void Attack()
        {
            _isAttacking = true;
            _animator.SetTrigger(AttackId);
            Idle();
        }

        public void StopDash()
        {
            _isDashing = false;
            _animator.SetBool(DashingId, false);
            _isIdle = true;
        }

        public void StopRun()
        {
            _isRunning = false;
            _animator.SetBool(RunningId, false);
            _isIdle = true;
        }


        private void StopIdle()
        {
            _isIdle = false;
        }
    }
}