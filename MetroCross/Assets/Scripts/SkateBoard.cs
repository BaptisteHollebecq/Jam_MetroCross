using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateBoard : MonoBehaviour
{
    public GameObject particle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController pc))
        {
            pc.GetSkate();
            particle.SetActive(true);
            Destroy(gameObject, 1);
        }
    }
}
