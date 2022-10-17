using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private const int BASE_OBJECTIVE_KILLS = 25;
        private int _objectiveKills;
        [SerializeField] private bool _isVictory;
        [SerializeField] private Dictionary<int, int> _enemiesKilled;

        private void Start()
        {
            _objectiveKills = BASE_OBJECTIVE_KILLS * (PlayerPrefs.GetInt("Difficulty", 0) +1);
            EventsManager.instance.EventRemainingKillsChange(0, _objectiveKills);
            _enemiesKilled = new Dictionary<int, int>();
            EventsManager.instance.OnGameOver += OnGameOver;
            EventsManager.instance.OnEnemyDeath += OnEnemyKilled;

            //lock cursor
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnGameOver(bool isVictory)
        {
            Time.timeScale = 0.2f;
            _isVictory = isVictory;
            GlobalDataManager.Instance.SetIsVictory(isVictory);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
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

            EventsManager.instance.EventRemainingKillsChange(totalEnemiesKilled, _objectiveKills);

            if (totalEnemiesKilled >= _objectiveKills)
            {
                OnGameOver(true);
            }
        }

        private void LoadEndgameScene()
        {
            Time.timeScale= 1;
            SceneManager.LoadScene("EndScene");

        }
    }
}