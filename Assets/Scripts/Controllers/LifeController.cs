using System;
using Strategies;
using UnityEngine;

namespace Controllers
{
    public class LifeController : MonoBehaviour, IDamageable
    {
        public float MaxHealth => GetComponent<Entity>().Stats.MaxHealth;
        [SerializeField] private float _currentLife;

        private void Start()
        {
            _currentLife = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentLife -= damage;
            if(_currentLife <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Destroy(this.gameObject);
        }
    }
}