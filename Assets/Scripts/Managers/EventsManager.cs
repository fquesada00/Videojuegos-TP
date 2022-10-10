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

    public event Action OnEnemyDeath;

    public void EnemyDeath()
    {
        OnEnemyDeath?.Invoke();
    }
}