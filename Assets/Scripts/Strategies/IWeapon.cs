using Controllers;
using UnityEngine;

public interface IWeapon
{

    WeaponStats WeaponStats { get; }  

    void Attack();

    // void PlayAnimation();
    AnimatorOverrideController OverrideController { get; }

    public float Damage();

    void OverrideAnimatorController();
}