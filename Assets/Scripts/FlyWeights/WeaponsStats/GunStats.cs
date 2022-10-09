using UnityEngine;

namespace FlyWeights.WeaponsStats
{
    [CreateAssetMenu(fileName = "GunStats", menuName = "Stats/Gun", order = 0)]
    public class GunStats : ScriptableObject
    {
        [SerializeField] private GunStatValues _statValues;

        public GameObject BulletPrefab => _statValues.BulletPrefab;
        public int Damage => _statValues.Damage;
        public float ShootCooldown => _statValues.ShootCooldown;
        public int MagSize => _statValues.MagSize;
    }
    
    [System.Serializable]
    public struct GunStatValues
    {
        public GameObject BulletPrefab;
        public int Damage;
        public float ShootCooldown;
        public int MagSize;
    }
}