using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text Timer;
    public TMP_InputField Pseudo;
    public TMP_InputField Ghost;

    public GameObject GameUI;
    public GameObject MenuUI;
    
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    
    void Update()
    {

    }

    public void OnPlay()
    {
        String pseudo = Pseudo.text.Replace(' ', '_');
        if (String.IsNullOrEmpty(pseudo)) return;

        Game.Instance.PlayerName = pseudo;
        Game.Instance.Ghost.PlayGhost = false;
        
        StartGame();
    }

    public void OnPlayVersus()
    {
        String pseudo = Pseudo.text.Replace(' ', '_');
        if (String.IsNullOrEmpty(pseudo)) return;
        
        Game.Instance.PlayerName = pseudo;
        
        String ghost = Ghost.text.Replace(' ', '_');
        if (String.IsNullOrEmpty(pseudo)) return;
        
        Game.Instance.Ghost.GhostName = ghost;
        Game.Instance.Ghost.PlayGhost = true;
        
        StartGame();
    }

    public void StartGame()
    {
        MenuUI.SetActive(false);
        GameUI.SetActive(true);
        Game.Instance.Playing = true;

        Game.Instance.StartGame();
    }

    public void UpdateTimer(float time)
    {
        string minute = ""+Mathf.Floor(time / 60);
        minute = minute.PadLeft(2, '0');
        
        string secondes = String.Format("{0:0.00}", time % 60);
        secondes = secondes.PadLeft(5, '0');

        Timer.text = minute + ":" + secondes;

    }
}
