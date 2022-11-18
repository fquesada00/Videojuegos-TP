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

        private IGun _owner;
        public IGun Owner => _owner;

        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody;

        private Collider _collider;
        public Collider Collider => _collider;

        protected String _collisionTag = "Enemy";

        public ParticleSystem HitEffect;

        public String CollisionTag
        {
            get => _collisionTag;
            set => _collisionTag = value;
        }

        private float _damage;
        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        private bool _crit;
        public bool Crit
        {
            get => _crit;
            set => _crit = value;
        }
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            _collider.isTrigger = true;
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = false;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

            _rigidbody.velocity = transform.forward * _speed;

            Destroy(gameObject, _lifeTime);
        }


        public void Travel()
        {
            //transform.Translate(Vector3.forward * (Time.deltaTime * Speed));
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(_collisionTag))
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage, Crit);
            }
            if(HitEffect != null){
                GameObject hitEffect = Instantiate(HitEffect.gameObject, transform.position, Quaternion.identity);
                 Destroy(hitEffect, 1f);
            }
            Destroy(this.gameObject);
        }

        public void SetOwner(IGun owner) => _owner = owner;
        public void SetSpeed(float speed) => _speed = speed;
        public void SetLifeTime(float lifeTime) => _lifeTime = lifeTime;
    }
}