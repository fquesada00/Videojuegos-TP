using UnityEngine;

namespace FlyWeights.WeaponsStats
{
    [CreateAssetMenu(fileName = "BulletStats", menuName = "Stats/Bullet", order = 0)]
    public class BulletStats : ScriptableObject
    {
        [SerializeField] private BulletStatValues _statValues;

        public int LifeTime => _statValues.LifeTime;
        public float Speed => _statValues.Speed;
    }
    
    [System.Serializable]
    public struct BulletStatValues
    {
        public int LifeTime;
        public float Speed;
    }
}