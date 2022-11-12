using System;
using System.Collections;
using System.Collections.Generic;
using Commands.Sounds;
using Controllers;
using UnityEngine;

[RequireComponent(typeof(SoundController), typeof(AnimatorOverrideController))]
public abstract class Weapon : MonoBehaviour, IWeapon
{
    public virtual void Attack()
    {
        _cmdAttackSound.Execute();
    }
    public void OverrideAnimatorController()
    {
        _animationOverriderController.SetOverrideController(_animatorOverrideController);
    }
    
    public AnimatorOverrideController OverrideController => _animatorOverrideController;
    [SerializeField] private AnimatorOverrideController _animatorOverrideController;
    
    private AnimationOverriderController _animationOverriderController;
    
    public SoundController SoundController => _soundController;
    [SerializeField] private SoundController _soundController;
    private CmdAttackSound _cmdAttackSound;

    public abstract WeaponStats WeaponStats { get; }

    public float Damage(){
        //10% chance to crit
        if (UnityEngine.Random.Range(0,10) == 0)
        {
            return WeaponStats.Damage * 2;
        }
        return WeaponStats.Damage;
    }

    protected void Start()
    {
        // look up for the animation controller
        _animationOverriderController = GetComponentInParent<AnimationOverriderController>();
        _soundController = GetComponent<SoundController>();
        _cmdAttackSound = new CmdAttackSound(_soundController);
    }


}

