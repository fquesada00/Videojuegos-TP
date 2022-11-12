using System;
using System.Collections;
using System.Collections.Generic;
using Commands.Sounds;
using Controllers;
using UnityEngine;

[RequireComponent(typeof(SoundController))]
public abstract class Weapon : MonoBehaviour, IWeapon
{
    public virtual void Attack(bool crit)
    {
        _cmdAttackSound.Execute();
    }
    
    
    public SoundController SoundController => _soundController;
    [SerializeField] private SoundController _soundController;
    private CmdAttackSound _cmdAttackSound;

    public abstract WeaponStats WeaponStats { get; }

    protected void Start()
    {
        // look up for the animation controller
        _soundController = GetComponent<SoundController>();
        _cmdAttackSound = new CmdAttackSound(_soundController);
    }


}

