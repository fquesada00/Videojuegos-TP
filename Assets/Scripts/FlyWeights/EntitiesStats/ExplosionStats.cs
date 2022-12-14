using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategies;

namespace FlyWeights.EntitiesStats
{
    [CreateAssetMenu(fileName = "ExplosionStats", menuName = "Stats/Explosion", order = 0)]
    public class ExplosionStats : ScriptableObject
    {
        [SerializeField] private ExplosionStatsValues _explosionStatValues;
        public float Range => _explosionStatValues.Range;
        public float Damage => _explosionStatValues.Damage;
    }

    [System.Serializable]
    public struct ExplosionStatsValues
    {
        public float Range;
        public float Damage;
    }
}
