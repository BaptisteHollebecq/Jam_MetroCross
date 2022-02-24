using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Components")]
    public PlayerController Player;
    public Phantom Ghost;
    public Transform StartPosition;

    [Header("Settings")] 
    public string PlayerName;
    
    [HideInInspector] public bool Playing;
    public static Game Instance;

    private void Awake()
    {
        Instance = this;
        Player.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        Player.gameObject.SetActive(true);
        Player.transform.position = StartPosition.position;
        Playing = true;

        Ghost.StartGhost();
    }

    public void EndLevel()
    {
        Playing = false;
        
        Ghost.SaveFile(PlayerName);
    }
}
