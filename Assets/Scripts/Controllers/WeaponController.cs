using System;
using System.Collections;
using System.Collections.Generic;
using Controllers.Utils;
using Strategies;
using UnityEngine;
using Weapons;

namespace Controllers
{
    public class WeaponController : MonoBehaviour, IAttackable
    {
        [SerializeField] private List<Weapon> _weapons;

        private IWeapon _currentWeapon;
        public IWeapon CurrentWeapon => _currentWeapon;

        private AnimationController _animationController;
        private Cooldown _gunAnimationCooldown;

        private void Start()
        {
            _animationController = GetComponentInChildren<AnimationController>();
            _gunAnimationCooldown = new Cooldown();
        }

        public void Attack()
        {
            // if (_gunAnimationCooldown.IsOnCooldown() && _currentWeapon is Gun) return;

            // first set animator controller
            // _currentWeapon.OverrideAnimatorController();
            _animationController.Attack();

            // if current weapon is a gun, then animate, wait for animation to finish, then shoot
            // if (_currentWeapon is Gun)
            // {
            //     float animationLength = 1;//_animationController.GetCurrentAnimationLength();
            //     StartCoroutine(
            //         _gunAnimationCooldown.CallbackCooldown(animationLength, () => _currentWeapon.Attack()));
            // }
            // else
            // {
            //     _currentWeapon.Attack();
            // }
            
            _currentWeapon.Attack();
        }

        public void SwitchWeapon(int index)
        {
            if (index < 0 || index >= _weapons.Count)
            {
                return;
            }

            for (int i = 0; i < _weapons.Count; i++)
            {
                _weapons[i].gameObject.SetActive(i == index);
            }

            _currentWeapon = _weapons[index];
            _currentWeapon.OverrideAnimatorController();
        }
    }
}