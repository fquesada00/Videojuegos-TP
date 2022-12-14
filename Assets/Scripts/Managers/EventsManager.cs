using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    public event Action<int, Killer> OnEnemyDeath;

    public void EnemyDeath(int enemyId, Killer killer)
    {
        OnEnemyDeath?.Invoke(enemyId, killer);
    }

    public event Action<bool> OnGameOver;

    public void GameOver(bool isVictory)
    {
        OnGameOver?.Invoke(isVictory);
    }
    public event Action OnEnemyObjectiveReach;
    
    public void EnemyObjectiveReach()
    {
        OnEnemyObjectiveReach?.Invoke();
    }

    public event Action<Difficulty> OnDifficultyChange;
    public void EventDifficultyChange(Difficulty difficulty)
    {
        if (OnDifficultyChange != null) OnDifficultyChange(difficulty);
    }

    public event Action OnBossKilled; 
    public void BossKilled()
    {
        OnBossKilled?.Invoke();
    }
    
    #region UI_HUD_EVENTS
    public event Action<int> OnWeaponChange;
    public event Action<float, float> OnCharacterLifeChange;
    public event Action<int, int> OnRemainingKillsChange;
    public event Action<float> OnSkillCooldownReduce;
    public event Action<int, float> OnSkillCooldownChange;
    public event Action<bool> OnPauseChange;
    public event Action<string> OnObjectiveChange;

    public void EventWeaponChange(int weaponIndex)
    {
        if (OnWeaponChange != null) OnWeaponChange(weaponIndex);
    }

    public void EventCharacterLifeChange(float currentLife, float maxLife)
    {
        if (OnCharacterLifeChange != null) OnCharacterLifeChange(currentLife, maxLife);
    }

    public void EventCooldownReduce(float timePassed)
    {
        if (OnSkillCooldownReduce != null) OnSkillCooldownReduce(timePassed);
    }

    public void EventSkillCooldownChange(int skillIndex, float cooldown)
    {
        if (OnSkillCooldownChange != null) OnSkillCooldownChange(skillIndex, cooldown);
    }

    public void EventRemainingKillsChange(int kills, int objectiveKills)
    {
        if (OnRemainingKillsChange != null) OnRemainingKillsChange(kills, objectiveKills);
    }

    public void EventPauseChange(bool isPaused)
    {
        if (OnPauseChange != null) OnPauseChange(isPaused);
    }

    public void EventObjectiveChange(string objectiveText)
    {
        if (OnObjectiveChange != null) OnObjectiveChange(objectiveText);
    }
    
    #endregion
}