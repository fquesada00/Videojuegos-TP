using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public SwordStats SwordStats => _swordStats;
    [SerializeField] private SwordStats _swordStats;

    public void Attack()
    {
        Debug.Log("Sword Attack");
    }

    

}
