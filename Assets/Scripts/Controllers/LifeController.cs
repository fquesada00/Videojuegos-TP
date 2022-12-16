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

        public Action<float> OnTakeDamage;

        public bool HasShield => _shield;
        [SerializeField] private bool _shield = false;

        private void Start()
        {
            _currentLife = MaxHealth;
            CallCharacterLifeChangeEvent();
        }

        public void TakeDamage(float damage, bool crit)
        {
            if (HasShield) return;

            damage = crit ? damage * 2 : damage;
            _currentLife -= damage;
            if (_currentLife < 0) _currentLife = 0;
            _healthDisplay?.TakeDamage(_currentLife, MaxHealth, damage, crit);
            CallCharacterLifeChangeEvent();
            OnTakeDamageEvent(damage);
            if (_currentLife <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            GetComponent<Entity>().Die();
            if (CompareTag("Player"))
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
            if ((_currentLife + health) > MaxHealth)
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
            if (CompareTag("Player"))
            {
                EventsManager.instance.EventCharacterLifeChange(_currentLife, MaxHealth);
            }
        }

        private void OnTakeDamageEvent(float damage)
        {
            OnTakeDamage?.Invoke(damage);
        }

        public void AddShield() => _shield = true;

        public void RemoveShield() => _shield = false;
    }
}