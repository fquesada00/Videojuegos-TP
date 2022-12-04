using System.Collections;
using System.Collections.Generic;
using Controllers.Utils;
using System;
using UnityEngine;
using Strategies;

namespace Controllers
{
    [RequireComponent(typeof(Entity))]
    public class SkillsController : MonoBehaviour, ISkills
    {
        [SerializeField] private Weapon _boomerang;
        public float BoomerangCooldown => GetComponent<Actor>().ActorStats.BoomerangCooldown;
        private Cooldown _boomerangCooldown = new Cooldown();

        public void throwBoomerang()
        {
            if (_boomerangCooldown.IsOnCooldown()) return;
            
            // EventsManager.instance.EventSkillCooldownChange(2, BoomerangCooldown);

            _boomerang.Attack(false);

            StartCoroutine(_boomerangCooldown.BooleanCooldown(BoomerangCooldown));
        }
    }
}
