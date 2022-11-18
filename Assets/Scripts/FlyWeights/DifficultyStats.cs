using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyStats", menuName = "Stats/Difficulty", order = 0)]
public class DifficultyStats : ScriptableObject
{
    [SerializeField] private DifficultyStatsValues _difficultyStatsValues;
}

[System.Serializable]
public struct DifficultyStatsValues
{
    public int MaxSimultaneousEnemiesSize;
    public int EnemiesPerBatchSize;
    public int ObjectiveKills;
}
