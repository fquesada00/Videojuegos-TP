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

        public override void Attack()
        {

            base.Attack();

            //create phyisics raycast
            //if hit enemy
            //  damage enemy
           
            /*RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1000)) //TODO HARDCODED 1000
            {
                Debug.Log("Hit something");
                IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage);
                //create line renderer
                LineRenderer lineRenderer = GetComponent<LineRenderer>();
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
            }*/

        }
    }
}