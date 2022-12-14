using System.Collections;
using System.Collections.Generic;
using Commands.Sounds;
using Controllers;
using UnityEngine;
using Strategies;

public abstract class Entity : MonoBehaviour
{
    public abstract EntityStats Stats { get; }

    private SoundController _soundController;

    public SoundController SoundController
    {
        get => _soundController;
        set => _soundController = value;
    }

    public virtual void Die(Killer killer = Killer.PLAYER)
    {
        Destroy(gameObject);
        DeathSound();
    }

    protected void DeathSound()
    {
        if (_soundController != null)
            new CmdDeathSound(SoundController).Execute();
    }

}

public enum Killer
{
    PLAYER,
    ENEMY,
    NONE
}