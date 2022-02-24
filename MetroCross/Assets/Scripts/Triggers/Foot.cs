using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    private PlayerController _controller;
    private void Start()
    {
        _controller = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _controller.OnFootTriggerStay(other);
    }
}
