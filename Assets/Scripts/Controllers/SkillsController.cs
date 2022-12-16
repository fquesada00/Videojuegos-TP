using System.Collections;
using System.Collections.Generic;
using Utils;
using System;
using UnityEngine;
using Strategies;
using Weapons;

namespace Controllers
{
    [RequireComponent(typeof(Entity))]
    public class SkillsController : MonoBehaviour, ISkills
    {
        [SerializeField] private Boomerang _boomerang;
        public float BoomerangCooldown => GetComponent<Actor>().ActorStats.BoomerangCooldown;
        private Cooldown _boomerangCooldown = new Cooldown();

        public void throwBoomerang()
        {
            if (_boomerangCooldown.IsOnCooldown() || !_boomerang.isAvailable()) return;
            
            EventsManager.instance.EventSkillCooldownChange(1, BoomerangCooldown);

            _boomerang.Attack(false);

            StartCoroutine(_boomerangCooldown.BooleanCooldown(BoomerangCooldown));
        }
    }
}
