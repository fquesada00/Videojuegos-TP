using Strategies;
using UnityEngine;

namespace FlyWeights.EntitiesStats
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/Enemy", order = 0)]
    public class EnemyStats : EntityStats
    {
        [SerializeField] private EnemyStatsValues _enemyStatValues;
        
        public GameObject PotionPrefab => _enemyStatValues.PotionPrefab;
    }
    
    [System.Serializable]
    public struct EnemyStatsValues
    {
        public GameObject PotionPrefab;
    }
}