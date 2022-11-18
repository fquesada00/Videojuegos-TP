using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyStats", menuName = "Stats/Difficulty", order = 0)]
public class DifficultyStats : ScriptableObject
{
    [SerializeField] private DifficultyStatsValues _difficultyStatsValues;
    public int MaxSimultaneousEnemiesSize => _difficultyStatsValues.MaxSimultaneousEnemiesSize;

    public int EnemiesPerBatchSize => _difficultyStatsValues.EnemiesPerBatchSize;

    public int ObjectiveKills => _difficultyStatsValues.ObjectiveKills;
    
    public float EnemyDamageMultiplier => _difficultyStatsValues.EnemyDamageMultiplier;

}

[System.Serializable]
public struct DifficultyStatsValues
{
    public int MaxSimultaneousEnemiesSize;
    public int EnemiesPerBatchSize;
    public int ObjectiveKills;
    public float EnemyDamageMultiplier;
}
