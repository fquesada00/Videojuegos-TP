using UnityEngine;
using FlyWeights.WeaponsStats;
namespace Strategies
{
    public interface IGun : IWeapon
    {
        GameObject BulletPrefab { get; }

        GunStats Stats { get; }  
    
        int BulletCount { get; }
    }
}