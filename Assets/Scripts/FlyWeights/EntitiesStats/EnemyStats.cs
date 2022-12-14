using Strategies;
using UnityEngine;

namespace FlyWeights.EntitiesStats
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/Enemy", order = 0)]
    public class EnemyStats : EntityStats
    {
        [SerializeField] private EnemyStatsValues _enemyStatValues;
        public float AttackRange => _enemyStatValues.AttackRange;
        public float MinSoundDistance => _enemyStatValues.MinSoundDistance;
        public float SoundCooldown => _enemyStatValues.SoundCooldown;
    }
    
    [System.Serializable]
    public struct EnemyStatsValues
    {
        public float AttackRange;
        public float MinSoundDistance;
        public float SoundCooldown;

    }
}