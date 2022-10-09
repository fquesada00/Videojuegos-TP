using UnityEngine;

public interface IWeapon
{
    public void Attack();

    // void PlayAnimation();
    AnimatorOverrideController OverrideController { get; }
}