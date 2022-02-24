using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PlayerController Player;

    public static Game Instance;

    private void Awake()
    {
        Instance = this;
    }
}
