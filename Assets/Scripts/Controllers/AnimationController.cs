using System;
using Strategies;
using UnityEngine;
using Weapons;

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

        public GameObject GunLocation;
        private double vanashingTime = 1.0;
        private double vanashing = 0;
        private Weapon weapon;

        Vector3 lastPosition;
        float lastTime;
        void OnAnimatorIK()
        {

            if (weapon is Gun && vanashing > 0) //weapon is gun
            {

                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

                //lerp from lastPosition to GunLocation
                //_animator.SetIKPosition(AvatarIKGoal.RightHand, Vector3.Lerp(lastPosition, GunLocation.transform.position, (float)(20f*(Time.time - lastTime)/vanashingTime)));
                _animator.SetIKPosition(AvatarIKGoal.RightHand, Vector3.Lerp(lastPosition, GunLocation.transform.position, (Time.time - lastTime)/0.06f));

                //_animator.SetIKPosition(AvatarIKGoal.RightHand, GunLocation.transform.position);
                _animator.SetIKRotation(AvatarIKGoal.RightHand, GunLocation.transform.rotation);
            }
            else
            {
                lastPosition = _animator.GetIKPosition(AvatarIKGoal.RightHand);
                lastTime = Time.time;
            }
        }


        public void Update()
        {
            vanashing -= Time.deltaTime;
        }

        public void setGrounded(bool grounded)
        {
            _animator.SetBool(GroundedId, grounded);
        }


        public void Attack()
        {
            vanashing = vanashingTime;
            _animator.SetTrigger(AttackId);
        }



        public void Run(float speed)
        {
            _animator.SetFloat(SpeedId, speed);
        }

        public void SetWeapon(int index, Weapon weapon)
        {
            vanashingTime = weapon.WeaponStats.VanashingTime;
            this.weapon = weapon;
            _animator.SetInteger("weapon", index);
        }
    }
}