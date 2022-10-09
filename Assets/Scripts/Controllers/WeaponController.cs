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

        private void Start() {
            // FIXME: add from UI
            //_weapons = new List<IWeapon>();
            SwitchWeapon(0);
        }

        public void Attack()
        {   
            _currentWeapon.Attack();
        }

        public void SwitchWeapon(int index) {
            if (index < 0 || index >= _weapons.Count) {
                return;
            }

            _currentWeapon = _weapons[index];
        }
    }
}
