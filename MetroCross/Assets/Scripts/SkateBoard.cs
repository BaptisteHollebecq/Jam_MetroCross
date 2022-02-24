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
            StartCoroutine(DestroySkate());

        }
    }

    IEnumerator DestroySkate()
    {
        yield return new WaitForSeconds(1);
        particle.SetActive(false);
        gameObject.SetActive(false);
        
    }
}
