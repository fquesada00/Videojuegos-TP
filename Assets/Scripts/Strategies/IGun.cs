using UnityEngine;

namespace Strategies
{
    public interface IGun : IWeapon
    {
        GameObject BulletPrefab { get; }
    
        int BulletCount { get; }
    }
}