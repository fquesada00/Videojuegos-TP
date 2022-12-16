using FlyWeights.WeaponsStats;
using Strategies;
using Managers;
using UnityEngine;

public class CannonballThrower : Weapon
{        
    public override WeaponStats WeaponStats => _stats;
    [SerializeField] private GunStats _stats;
    [SerializeField]  GameObject BulletPrefab;
    private Vector3 _target;
    private float _flightTime = 3f;
    private float _angle = Mathf.PI / 4f; 
    public Vector3 Target
    {
        get => _target;
        set => _target = value;
    }

    public override void Attack(bool crit)
    {
        base.Attack(crit);
        // get the gun hole position

        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.LookRotation(transform.forward));

        float distance = Vector3.Distance(transform.position, _target);
        float deltaY = _target.y - transform.position.y;
        float range = Vector3.Distance(transform.position, _target - deltaY * Vector3.up); // delta horizontal
        float V2 = 0.5f * Physics.gravity.y * Mathf.Pow(range,2);
        V2 /= deltaY - Mathf.Tan(_angle)*range;
        V2 /= Mathf.Pow(Mathf.Cos(_angle),2);
        float V = Mathf.Sqrt(V2);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * V * Mathf.Cos(_angle) + transform.up * V * Mathf.Sin(_angle);
    }
}
