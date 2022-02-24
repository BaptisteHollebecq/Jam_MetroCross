using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Components")]
    public PlayerController Player;
    public Phantom Ghost;

    [Header("Settings")] 
    public string PlayerName;
    
    [HideInInspector] public bool Playing;
    public static Game Instance;

    private void Awake()
    {
        Instance = this;
        Playing = true;
    }

    public void EndLevel()
    {
        Playing = false;
        
        Ghost.SaveFile(PlayerName);
    }
}
