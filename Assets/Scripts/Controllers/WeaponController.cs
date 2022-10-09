using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class WeaponController : MonoBehaviour, IAttackable
    {


        [SerializeField] private List<Weapon> _weapons; 
    
        private IWeapon _currentWeapon;
        public IWeapon CurrentWeapon => _currentWeapon;

        public void Attack()
        {   
            _currentWeapon.Attack();
        }

        public void SwitchWeapon(int index) {
            if (index < 0 || index >= _weapons.Count) {
                return;
            }
            
            for (int i = 0; i < _weapons.Count; i++) {
                _weapons[i].gameObject.SetActive(i == index);
            }

            _currentWeapon = _weapons[index];
        }
    }
}
