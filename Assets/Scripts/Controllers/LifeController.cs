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

        private void Start()
        {
            _currentLife = MaxHealth;
            CallCharacterLifeChangeEvent();
        }

        public void TakeDamage(float damage)
        {
            _currentLife -= damage;
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
                EventsManager.instance.EventCharacterLifeChange(_currentLife, MaxHealth); 
        }
    }
}