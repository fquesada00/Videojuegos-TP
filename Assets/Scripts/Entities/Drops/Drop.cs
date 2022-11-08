using Controllers;
using FlyWeights;
using Strategies;
using UnityEngine;

namespace Entities.Drops
{
    public abstract class Drop : MonoBehaviour, IDrop
    {
        public DropStats DropStats => _dropStats;
        [SerializeField] private DropStats _dropStats;
        
        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody;

        private Collider _collider;
        public Collider Collider => _collider;
        
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            _collider.isTrigger = true;
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                Take(damageable);
                Destroy(gameObject);
            }
        }

        public abstract void Take(IDamageable damageable);
    }
}