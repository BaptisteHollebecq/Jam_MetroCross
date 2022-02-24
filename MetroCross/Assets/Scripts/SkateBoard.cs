using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateBoard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController pc))
        {
            pc.GetSkate();
            Destroy(gameObject);
        }
    }
}
