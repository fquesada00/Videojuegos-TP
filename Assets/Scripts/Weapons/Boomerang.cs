using System.Collections;
using System.Collections.Generic;
using Strategies;
using UnityEngine;

namespace Weapons
{
    public class Boomerang : Weapon
    {
        [SerializeField] private BoomerangStats _stats;
        [SerializeField] private TrailRenderer _trail;
        public BoomerangStats BoomerangStats => _stats;
        public override WeaponStats WeaponStats => _stats;
        public float MaxSpeed => BoomerangStats.MaxSpeed;
        public float Range => BoomerangStats.Range;

        private float _speed = 0;
        private float _acceleration = 0;
        private Transform _parent;
        private Vector3 _idlePosition;
        private Quaternion _idleRotation;
        private Vector3 _attackDirection;
        private BoomerangState _state;
        private Collider _collider;

        private new void Start()
        {
            base.Start();
            _parent = transform.parent;
            _state = BoomerangState.IDLE;
            _idlePosition = transform.localPosition;
            _idleRotation = transform.localRotation;
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
            _acceleration = Mathf.Pow(MaxSpeed, 2) / (2 * Range);
            _trail.enabled = false;
        }

        void Update()
        {
            if (_state == BoomerangState.IDLE) return;

            transform.Rotate(Vector3.up * 10 * Time.deltaTime);
            Vector3 direction;
            float distance = Vector3.Distance(transform.position, _parent.position);
            switch (_state)
            {
                case BoomerangState.GOING:
                    direction = _attackDirection;
                    if(distance > Range || _speed < 0) {
                        _state = BoomerangState.RETURNING;
                    }
                    break;
                case BoomerangState.RETURNING:
                    direction = transform.position - _parent.position; // speed when returning is negative
                    if(distance < 0.5) {
                        transform.parent = _parent;
                        transform.localPosition = _idlePosition;
                        transform.localRotation = _idleRotation;
                        _state = BoomerangState.IDLE;
                        _collider.enabled = false;
                        _speed = 0;
                        _trail.enabled = false;
                        return;
                    }
                    break;

                default:
                    throw new System.NotImplementedException();
            }

            _speed -= _acceleration * Time.deltaTime;
            transform.position += direction.normalized * Time.deltaTime * _speed;
        }

        public override void Attack(bool crit)
        {
            if (_state == BoomerangState.IDLE)
            {
                _trail.enabled = true;
                _speed = MaxSpeed;
                _collider.enabled = true;
                _attackDirection = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 100)) - transform.position;
                _state = BoomerangState.GOING;
                transform.parent = null;
                transform.rotation = Quaternion.LookRotation(_attackDirection);
                base.Attack(false);
            }
        }

        public bool isAvailable()
        {
            return _state == BoomerangState.IDLE;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(WeaponStats.Damage, false);
            } else if (other.gameObject.isStatic) {
                _state = BoomerangState.RETURNING;
            }
        }
    }

    public enum BoomerangState
    {
        RETURNING,
        GOING,
        IDLE
    }
}