﻿using System.Collections;
using UnityEngine;

namespace Controllers.Utils
{
    public class Cooldown
    {
        private bool _isOnCooldown = false;
        
        public IEnumerator BooleanCooldown (float coolDownTime)
        {
            _isOnCooldown = true;
            yield return new WaitForSeconds(coolDownTime);
            _isOnCooldown = false;
        }
        
        public bool IsOnCooldown() => _isOnCooldown;
        
    }
}