using UnityEngine;

namespace FlyWeights.WeaponsStats
{
    [CreateAssetMenu(fileName = "GunStats", menuName = "Stats/Gun", order = 0)]
    public class GunStats : WeaponStats
    {
        [SerializeField] private GunStatValues _statValues;

        public GameObject BulletPrefab => _statValues.BulletPrefab;

        public int MagSize => _statValues.MagSize;
    }
    
    [System.Serializable]
    public struct GunStatValues
    {
        public GameObject BulletPrefab;
        public int MagSize;
    }
}