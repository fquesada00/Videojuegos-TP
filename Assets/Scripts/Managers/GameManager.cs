using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool _isGameOver;
        [SerializeField] private bool _isVictory;
        
        [SerializeField] private Dictionary<int, int> _enemiesKilled;

        private void Start()
        {
            EventsManager.instance.OnGameOver += OnGameOver;
            _enemiesKilled = new Dictionary<int, int>();
            EventsManager.instance.OnEnemyDeath += OnEnemyKilled;
        }

        private void OnGameOver(bool isVictory)
        {
            _isGameOver = true;
            _isVictory = isVictory;
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
        }
    }
}