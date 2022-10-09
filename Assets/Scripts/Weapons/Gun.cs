using Entities;
using FlyWeights.WeaponsStats;
using Strategies;
using UnityEngine;

namespace Weapons
{
    public class Gun : Weapon, IGun
    {
        public GunStats Stats => _stats;
        [SerializeField] private GunStats _stats;
        [SerializeField] private BulletStats _bulletStats;

        public GameObject BulletPrefab => _stats.BulletPrefab;
        public float Damage => _stats.Damage;
        private int _bulletCount;
        public int BulletCount => _bulletCount;

        private new void Start()
        {
            base.Start();
            
            _bulletCount = _stats.MagSize;
        }

        public override void Attack()
        {
            Debug.Log(transform.position);
            // get the gun hole position
            var gunHoleTransform = transform.Find("Gun_Bullet_Hole");
            
            var bullet = Instantiate(BulletPrefab, gunHoleTransform.position, gunHoleTransform.rotation);
            bullet.name = "Bullet";
            IBullet iBullet = bullet.GetComponent<IBullet>();
            iBullet.SetOwner(this);
            iBullet.SetSpeed(_bulletStats.Speed);
            iBullet.SetLifeTime(_bulletStats.LifeTime);
            
            _bulletCount--;
        }
    }
}