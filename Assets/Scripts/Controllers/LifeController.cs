using System;
using Strategies;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Entity))]
    public class LifeController : MonoBehaviour, IDamageable
    {
        public float MaxHealth => GetComponent<Entity>().Stats.MaxHealth;
        [SerializeField] private float _currentLife;

        [SerializeField] private HealthDisplay _healthDisplay;

        private void Start()
        {
            _currentLife = MaxHealth;
            CallCharacterLifeChangeEvent();
        }

        public void TakeDamage(float damage)
        {
            _currentLife -= damage;
            _healthDisplay?.setLife(_currentLife, MaxHealth);
            CallCharacterLifeChangeEvent();
            if(_currentLife <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            GetComponent<Entity>().Die();
            CallCharacterLifeChangeEvent();
            if(tag == "Player")
            {
                EventsManager.instance.GameOver(false);
            }
        }

        public void ResetLife()
        {
            _currentLife = MaxHealth;
        }
        
        public void AddHealth(float health)
        {
            if((_currentLife + health) > MaxHealth)
            {
                _currentLife = MaxHealth;
            }
            else
            {
                _currentLife += health;
            }
            CallCharacterLifeChangeEvent();
        }

        private void CallCharacterLifeChangeEvent()
        {
            if(tag == "Player")
            {
                EventsManager.instance.EventCharacterLifeChange(_currentLife, MaxHealth); 
            }
        }
    }
}