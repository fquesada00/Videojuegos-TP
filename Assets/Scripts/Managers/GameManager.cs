using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool _isVictory;
        
        [SerializeField] private Dictionary<int, int> _enemiesKilled;

        private void Start()
        {
            _enemiesKilled = new Dictionary<int, int>();
            EventsManager.instance.OnGameOver += OnGameOver;
            EventsManager.instance.OnEnemyDeath += OnEnemyKilled;
        }

        private void OnGameOver(bool isVictory)
        {
            _isVictory = isVictory;
            GlobalDataManager.Instance.SetIsVictory(isVictory);
            
            Invoke("LoadEndgameScene", 1); // TODO: MAGIC NUMBER
        }
        
        private void OnEnemyKilled(int enemyId)
        {
            if (_enemiesKilled.ContainsKey(enemyId))
            {
                _enemiesKilled[enemyId]++;
            }
            else
            {
                _enemiesKilled.Add(enemyId, 1);
            }
            
            int totalEnemiesKilled = 0;
            foreach (int timesKilledEnemy in _enemiesKilled.Values)
            {
                totalEnemiesKilled += timesKilledEnemy;
            }

            if (totalEnemiesKilled > 3) // TODO: MAGIC NUMBER
            {
                OnGameOver(true);
            }
        }

        private void LoadEndgameScene() => SceneManager.LoadScene("EndScene");
    }
}