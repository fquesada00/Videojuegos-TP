using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPedestal : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(false);
        EventsManager.instance.OnBossKilled += OnBossKilled;
    }

    private void OnBossKilled()
    {
        this.gameObject.SetActive(true);
    }

    // Trigger Change Scene on player collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventsManager.instance.PlayerEnterPortal();
        }
    }

}
