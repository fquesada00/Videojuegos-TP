using Strategies;
using UnityEngine;

namespace FlyWeights.EntitiesStats
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/Enemy", order = 0)]
    public class EnemyStats : EntityStats
    {
        [SerializeField] private EnemyStatsValues _enemyStatValues;

    }
    
    [System.Serializable]
    public struct EnemyStatsValues
    {
    }
}