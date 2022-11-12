using UnityEngine;
using FlyWeights.WeaponsStats;
namespace Strategies
{
    public interface IGun : IWeapon
    {
        GameObject BulletPrefab { get; }
    
        int BulletCount { get; }
    }
}