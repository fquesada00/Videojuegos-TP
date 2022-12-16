using System.Collections;
using UnityEngine;

namespace Utils
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
        
        public IEnumerator BooleanCooldownRealtime (float length)
        {
            _isOnCooldown = true;
            yield return new WaitForSecondsRealtime(length);
            _isOnCooldown = false;
        }

        public IEnumerator CallbackCooldown (float coolDownTime, System.Action callback)
        {
            _isOnCooldown = true;
            yield return new WaitForSeconds(coolDownTime);
            callback();
            _isOnCooldown = false;
        }
        
        public IEnumerator CallbackCooldownRealtime (float length, System.Action callback)
        {
            _isOnCooldown = true;
            yield return new WaitForSecondsRealtime(length);
            callback();
            _isOnCooldown = false;
        }
        
        public bool IsOnCooldown() => _isOnCooldown;
        
        public void Reset() => _isOnCooldown = false;
    }
}