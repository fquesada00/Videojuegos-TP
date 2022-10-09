using System;
using Strategies;
using UnityEngine;

namespace Controllers
{
    public class LifeController : MonoBehaviour, IDamageable
    {
        public float MaxLife => GetComponent<Actor>().ActorStats.MaxLife;
        [SerializeField] private float _currentLife;

        private void Start()
        {
            _currentLife = MaxLife;
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