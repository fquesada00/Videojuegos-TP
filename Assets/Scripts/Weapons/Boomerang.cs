using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Boomerang : Weapon
    {
        private float _speed;
        public float Speed => _speed;
        
        private float _range;
        public float Range => _range;
        private Transform _parent;
        private Vector3 _idlePosition;
        private Quaternion _idleRotation;
        private Vector3 _attackDirection;
        private BoomerangState _state;

        public override WeaponStats WeaponStats => throw new System.NotImplementedException();

        private new void Start()
        {
            _speed = 5f;
            _range = 15f;
            _parent = transform.parent;
            _state = BoomerangState.IDLE;
            _idlePosition = transform.localPosition;
            _idleRotation = transform.localRotation;
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
                    if(distance > Range) {
                        Debug.Log("Boomerang out of range");
                        _state = BoomerangState.RETURNING;
                    }
                    break;
                case BoomerangState.RETURNING:
                    direction = _parent.position - transform.position;
                    if(distance < 0.5) {
                        transform.parent = _parent;
                        transform.localPosition = _idlePosition;
                        transform.localRotation = _idleRotation;
                        _state = BoomerangState.IDLE;
                    }
                    break;

                default:
                    throw new System.NotImplementedException();
            }
            transform.position += direction.normalized * Time.deltaTime * Speed;
        }

        public override void Attack(bool crit)
        {
            if (_state == BoomerangState.IDLE)
            {
                _attackDirection = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 100)) - transform.position;
                _state = BoomerangState.GOING;
                transform.parent = null;
                transform.rotation = Quaternion.LookRotation(_attackDirection);
                base.Attack(crit);
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