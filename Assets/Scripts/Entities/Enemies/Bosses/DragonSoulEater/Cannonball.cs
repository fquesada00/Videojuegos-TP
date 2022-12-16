using UnityEngine;
using Controllers;
using FlyWeights.EntitiesStats;
using Managers;
using Utils;

public class Cannonball : MonoBehaviour
{
    [SerializeField] private GameObject _flyParticleSystems, _explodeStatusParticleSystems;
    [SerializeField] private ExplosionStats _explosionStats;

    private float _damageMultiplier;

    private void Start() 
    {
        _damageMultiplier = FindObjectOfType<GameManager>().GetCurrentDifficultyStats.EnemyDamageMultiplier;
        foreach (ParticleSystem particleSystem in _flyParticleSystems.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Play();
        }
        foreach (ParticleSystem particleSystem in _explodeStatusParticleSystems.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Stop();
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Collider>().enabled = false;
        // this.GetComponent<SoundController>().PlaySound("Explosion"); TODO: LO HACE OCTAVIO
        foreach (ParticleSystem particleSystem in _flyParticleSystems.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Stop();
        }
        foreach (ParticleSystem particleSystem in _explodeStatusParticleSystems.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Play();
        }

        StartCoroutine(new Cooldown().CallbackCooldown(0.5f, () => 
            ExplosionRaycast.Explode(transform.position, this._explosionStats.Range, this._explosionStats.Damage * _damageMultiplier)
        ));
        StartCoroutine(new Cooldown().CallbackCooldown(2f, () => Destroy(gameObject)));
    }
}
