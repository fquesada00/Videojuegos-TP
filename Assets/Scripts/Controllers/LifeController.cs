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

        public void TakeDamage(float damage, bool crit)
        {
            damage = crit ? damage * 2 : damage;
            _currentLife -= damage;
            if(_currentLife < 0) _currentLife = 0;
            _healthDisplay?.TakeDamage(_currentLife, MaxHealth, damage, crit);
            CallCharacterLifeChangeEvent();
            if(_currentLife <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            GetComponent<Entity>().Die();
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