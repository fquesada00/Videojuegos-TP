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

    public event Action<int> OnEnemyDeath;

    public void EnemyDeath(int enemyId)
    {
        OnEnemyDeath?.Invoke(enemyId);
    }

    public event Action<bool> OnGameOver;

    public void GameOver(bool isVictory)
    {
        OnGameOver?.Invoke(isVictory);
    }
    
    #region UI_HUD_EVENTS
    public event Action<int, int> OnAmmoChange;
    public event Action<int> OnWeaponChange;
    public event Action<float, float> OnCharacterLifeChange;
    
    public void EventAmmoChange(int currentAmmo, int maxAmmo)
    {
        if (OnAmmoChange != null) OnAmmoChange(currentAmmo, maxAmmo);
    }

    public void EventWeaponChange(int weaponIndex)
    {
        if (OnWeaponChange != null) OnWeaponChange(weaponIndex);
    }

    public void EventCharacterLifeChange(float currentLife, float maxLife)
    {
        if (OnCharacterLifeChange != null) OnCharacterLifeChange(currentLife, maxLife);
    }


    #endregion
}