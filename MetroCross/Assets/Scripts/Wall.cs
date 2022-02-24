using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private bool Hit;
    private void OnTriggerEnter(Collider other)
    {
        if (Hit) return;

        PlayerController player;
        if (other.TryGetComponent<PlayerController>(out player))
        {
            Hit = true;
            StartCoroutine(player.HitWall());
        }
    }
}
