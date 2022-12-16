using System;
using System.Collections;
using System.Collections.Generic;
using Commands.Sounds;
using Utils;
using Strategies;
using UnityEngine;
using Weapons;

namespace Controllers
{
    [RequireComponent(typeof(Entity))]
    public class WeaponController : MonoBehaviour, IAttackable
    {
        [SerializeField] private List<Weapon> _weapons;

        private Weapon _currentWeapon;
        public Weapon CurrentWeapon => _currentWeapon;

        private AnimationController _animationController;
        private Cooldown _gunAnimationCooldown;

        private Coroutine _vanishingCoroutine;
        
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
            if(_vanishingCoroutine != null)StopCoroutine(_vanishingCoroutine);
            float critProbability = GetComponent<Entity>().Stats.CritProbability;
            bool crit = UnityEngine.Random.Range(0.0f,1.0f) <= critProbability;
            _currentWeapon.gameObject.SetActive(true);
            _currentWeapon.Attack(crit);
            _vanishingCoroutine = StartCoroutine(new Cooldown().CallbackCooldown(_currentWeapon.WeaponStats.VanashingTime, () => _currentWeapon.gameObject.SetActive(false)));
        }

        public void SwitchWeapon(int index)
        {
            if (index < 0 || index >= _weapons.Count)
            {
                return;
            }

            for (int i = 0; i < _weapons.Count; i++)
            {
                _weapons[i].gameObject.SetActive(false);
            }

            _currentWeapon = _weapons[index];
            _animationController.SetWeapon(index, _currentWeapon);
        }
    }
}