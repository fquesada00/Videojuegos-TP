using Controllers;
using Utils;
using Strategies;
using UnityEngine;

namespace Entities
{
    public class PoisonSpell : MonoBehaviour, ISpell
    {
        [SerializeField] private GameObject _particleSystem;
        private Cooldown _cooldown;
        private float _damage;

        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        private float _range;

        public float Range
        {
            get => _range;
            set => _range = value;
        }

        private int _duration;

        public int Duration
        {
            get => _duration;
            set => _duration = value;
        }


        private void Start()
        {
            // start allways on the ground
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1000))
            {
                transform.position = hit.point;
            }
            
            _cooldown = new Cooldown();
            Destroy(gameObject, _duration);
            
            DisplayParticles();
            
        }

        private void Update()
        {
            Activate();
        }

        public void Activate()
        {
            Debug.Log("PoisonSpell.Activate()");
            if (_cooldown.IsOnCooldown()) return;
            StartCoroutine(_cooldown.CallbackCooldown(5f, Attack));
        }

        private void Attack()
        {
            Debug.Log("PoisonSpell.Attack()");
            // detect if the player is in range
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _range);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("PoisonSpell.Attack() - Player hit");
                    Debug.Log("PoisonSpell.Attack() - Damage: " + _damage);
                    hitCollider.gameObject.GetComponent<LifeController>().TakeDamage(_damage);
                    break;
                }
            }
        }

        private void DisplayParticles()
        {
            foreach (ParticleSystem particleSystem in _particleSystem.GetComponentsInChildren<ParticleSystem>())
            {
                ParticleSystem.MainModule main = particleSystem.main;
                // main.duration = _duration;
                if (particleSystem.name.StartsWith("Rays"))
                {
                    ParticleSystem.ShapeModule shape = particleSystem.shape;
                    shape.radius = _range * 2 * .5f;
                    main.startSize = .25f;
                } else if(particleSystem.name.StartsWith("Runes"))
                {
                    // TODO: scale the start size by range
                    main.startSize = _range * 2;
                }
                particleSystem.Play();
            }

            foreach (var light in _particleSystem.GetComponentsInChildren<Light>())
            {
                light.enabled = true;
            }
        }

        private void OnDestroy()
        {
            foreach (ParticleSystem particleSystem in _particleSystem.GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Stop();
            }

            foreach (var light in _particleSystem.GetComponentsInChildren<Light>())
            {
                light.enabled = false;
            }
        }
    }
}