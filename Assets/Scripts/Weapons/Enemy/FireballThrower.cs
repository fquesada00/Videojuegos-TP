using Entities;
using FlyWeights.WeaponsStats;
using Strategies;
using Controllers.NavMesh;
using Managers;
using UnityEngine;


public class FireballThrower : Weapon, IGun
{
    public override WeaponStats WeaponStats => _stats;
    public GunStats Stats => _stats;
    [SerializeField] private GunStats _stats;
    [SerializeField] private BulletStats _bulletStats;

    public GameObject BulletPrefab => _stats.BulletPrefab;
    private int _bulletCount;
    public int BulletCount => _bulletCount;
    private float _damageMultiplier = 1f;

    private GameObject _target;
    public GameObject Target
    {
        get => _target;
        set => _target = value;
    }


    private new void Start()
    {
        base.Start();

        _bulletCount = _stats.MagSize;
        _damageMultiplier = FindObjectOfType<GameManager>().GetCurrentDifficultyStats.EnemyDamageMultiplier;

    }

    public void Update()
    {
        //look at center of screen
        //var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        //transform.LookAt(ray.GetPoint(100));

    }

    public override void Attack(bool crit)
    {
        base.Attack(crit);
        // get the gun hole position

        var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.LookRotation(transform.forward));

        //aim to target
        bullet.transform.LookAt(Target.transform.position + Vector3.up * 1.5f);


        bullet.name = "Fireball";
        IBullet iBullet = bullet.GetComponent<IBullet>();
        iBullet.SetOwner(this);
        iBullet.CollisionTag = "Player";
        iBullet.SetSpeed(_bulletStats.Speed);
        iBullet.SetLifeTime(_bulletStats.LifeTime);
        iBullet.Damage = Stats.Damage * _damageMultiplier;
        iBullet.Crit = crit;

        _bulletCount--;
    }
}
