using FlyWeights.WeaponsStats;
using Strategies;
using UnityEngine;

namespace Weapons
{
    public class Gun : Weapon, IGun
    {
        public GunStats Stats => _stats;
        [SerializeField] private GunStats _stats;

        public GameObject BulletPrefab => _stats.BulletPrefab;
        public float Damage => _stats.Damage;
        private int _bulletCount;
        public int BulletCount => _bulletCount;

        private new void Start()
        {
            base.Start();
            
            _bulletCount = _stats.MagSize;
        }

        public void Attack()
        {
            base.Attack();
        }
    }
}