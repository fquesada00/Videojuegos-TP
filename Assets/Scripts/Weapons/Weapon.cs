using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public abstract void Attack();
    public void OverrideAnimatorController()
    {
        _animationOverriderController.SetOverrideController(_animatorOverrideController);
    }
    
    public AnimatorOverrideController OverrideController => _animatorOverrideController;
    [SerializeField] private AnimatorOverrideController _animatorOverrideController;
    
    private AnimationOverriderController _animationOverriderController;

    protected void Start()
    {
        // look up for the animation controller & set the attack animation
        _animationOverriderController = GetComponentInParent<AnimationOverriderController>();
        // _animationOverriderController.SetOverrideController(_animatorOverrideController);
    }
}

