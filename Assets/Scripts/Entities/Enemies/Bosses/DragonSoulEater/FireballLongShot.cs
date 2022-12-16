using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using Strategies;

public class FireballLongShot : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("FireballLongShot: OnCollisionEnter");
    }
}
