using Controllers;
using UnityEngine;

public interface IWeapon
{

    WeaponStats WeaponStats { get; }  

    void Attack(bool crit);

    // void PlayAnimation();
    AnimatorOverrideController OverrideController { get; }

    void OverrideAnimatorController();
}