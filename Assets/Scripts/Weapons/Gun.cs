using Entities;
using FlyWeights.WeaponsStats;
using Strategies;
using UnityEngine;

namespace Weapons
{
    public class Gun : Weapon, IGun
    {
        public override WeaponStats WeaponStats => _stats;
        public GunStats Stats => _stats;
        [SerializeField] private GunStats _stats;
        [SerializeField] private BulletStats _bulletStats;

        public GameObject BulletPrefab => _stats.BulletPrefab;
        private int _bulletCount;
        public int BulletCount => _bulletCount;

        private new void Start()
        {
            base.Start();
            
            _bulletCount = _stats.MagSize;
        }

        public void Update()
        {
            //look at center of screen
            var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            transform.LookAt(ray.GetPoint(100));
            
        }

        public override void Attack(bool crit)
        {
            base.Attack(crit);
            // get the gun hole position
            var gunHoleTransform = transform.Find("Gun_Bullet_Hole");
            
            var bullet = Instantiate(BulletPrefab, gunHoleTransform.position, Quaternion.LookRotation(gunHoleTransform.right));
            //bullet look at the center of the screen
            bullet.transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 100)));
            bullet.name = "Bullet";
            IBullet iBullet = bullet.GetComponent<IBullet>();
            iBullet.SetOwner(this);
            iBullet.SetSpeed(_bulletStats.Speed);
            iBullet.SetLifeTime(_bulletStats.LifeTime);
            iBullet.Damage = Stats.Damage;
            iBullet.Crit = crit;
            
            _bulletCount--;
        }
    }
}