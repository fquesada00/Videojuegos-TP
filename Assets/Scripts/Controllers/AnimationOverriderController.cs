using System;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Animator))]
    public class AnimationOverriderController : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void SetOverrideController(AnimatorOverrideController overrideController)
        {
            _animator.runtimeAnimatorController = overrideController;
        }
    }
}