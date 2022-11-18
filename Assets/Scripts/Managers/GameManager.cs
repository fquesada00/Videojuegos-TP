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
        [SerializeField] private DifficultyStats _easyDifficultyStats;
        [SerializeField] private DifficultyStats _mediumDifficultyStats;
        [SerializeField] private DifficultyStats _hardDifficultyStats;

        private DifficultyStats _currentDifficultyStats;
        public DifficultyStats GetCurrentDifficultyStats => _currentDifficultyStats;

        private void Awake()
        {
            _currentDifficultyStats = GetDifficultyStats();
        }

        private void Start()
        {
            _objectiveKills = _currentDifficultyStats.ObjectiveKills;
            EventsManager.instance.EventRemainingKillsChange(0, _objectiveKills);
            _enemiesKilled = new Dictionary<int, int>();
            EventsManager.instance.OnGameOver += OnGameOver;
            EventsManager.instance.OnEnemyDeath += OnEnemyKilled;
            
            // avoid collision with enemies
            Physics.IgnoreLayerCollision((int)Constants.Layers.PLAYER, (int)Constants.Layers.ENEMY, true);
            //lock cursor
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnGameOver(bool isVictory)
        {
            Time.timeScale = 0.2f;
            _isVictory = isVictory;
            GlobalDataManager.Instance.SetIsVictory(isVictory);
            // unlock cursor for end menu
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Invoke("LoadEndgameScene", 1); // TODO: MAGIC NUMBER
        }
        
        private void OnEnemyKilled(int enemyId, Killer killer)
        {
            if(killer != Killer.PLAYER) return;
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
        
        private DifficultyStats GetDifficultyStats()
        {
            Difficulty difficulty = GlobalDataManager.Instance.GetDifficulty();
            switch (difficulty)
            {
                case Difficulty.EASY:
                    return _easyDifficultyStats;
                case Difficulty.MEDIUM:
                    return _mediumDifficultyStats;
                case Difficulty.HARD:
                    return _hardDifficultyStats;
                default:
                    return _easyDifficultyStats;
            }
        }

    }
}