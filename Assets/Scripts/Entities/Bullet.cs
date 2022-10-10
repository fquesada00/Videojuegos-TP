using System;
using Strategies;
using UnityEngine;
using Weapons;

namespace Entities
{
    public class Bullet : MonoBehaviour, IBullet
    {
        private float _lifeTime;
        public float LifeTime => _lifeTime;

        private float _speed;
        public float Speed => _speed;

        private Gun _owner;
        public Gun Owner => _owner;

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
            _rigidbody.isKinematic = false;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

            _rigidbody.velocity = transform.forward * _speed;
        }

        private void Update()
        {
            _lifeTime -= Time.deltaTime;

            if (_lifeTime <= 0)
            {
                Destroy(this.gameObject);
            }

            //Travel();
        }

        public void Travel()
        {
            //transform.Translate(Vector3.forward * (Time.deltaTime * Speed));
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                damageable?.TakeDamage(_owner.Stats.Damage);
                Destroy(this.gameObject);
            }
            
            
        }

        public void SetOwner(Gun owner) => _owner = owner;
        public void SetSpeed(float speed) => _speed = speed;
        public void SetLifeTime(float lifeTime) => _lifeTime = lifeTime;
    }
}