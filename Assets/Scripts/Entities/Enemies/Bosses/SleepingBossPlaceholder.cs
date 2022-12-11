using System;
using System.Collections.Generic;
using Controllers.Utils;
using UnityEngine;

namespace Entities
{
    public class SleepingBossPlaceholder : MonoBehaviour
    {
        [SerializeField] private GameObject _boss;
        [SerializeField] private List<GameObject> _particleSystems;
        [SerializeField] private GameObject _beforeBossAppearParticleSystem;

        private void Start()
        {
            // disable boss
            _boss.SetActive(false);
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.SetActive(true);
            }
            _beforeBossAppearParticleSystem.SetActive(false);

            EventsManager.instance.OnEnemyObjectiveReach += OnEnemyObjectiveReach;
        }

        private void OnEnemyObjectiveReach()
        {
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.SetActive(false);
            }
            _beforeBossAppearParticleSystem.SetActive(true);
            StartCoroutine(new Cooldown().CallbackCooldown(2f, BeforeBossAppearance));
        }


        private void BeforeBossAppearance()
        {            
            _boss.SetActive(true);
            _beforeBossAppearParticleSystem.SetActive(false);

            // get sleeping boss by tag and destroy it
            Destroy(GameObject.FindGameObjectWithTag("SleepingBoss"));
        }
    }
}