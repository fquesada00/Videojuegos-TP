using Controllers;
using UnityEngine;

public interface IWeapon
{
    void Attack();

    // void PlayAnimation();
    AnimatorOverrideController OverrideController { get; }

    void OverrideAnimatorController();
    
    SoundController SoundController { get; }
}